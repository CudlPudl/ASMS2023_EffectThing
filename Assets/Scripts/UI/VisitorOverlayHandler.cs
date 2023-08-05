using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEngine;

public class VisitorOverlayHandler : MonoBehaviour
{
    [SerializeField] private Transform _root;
    [SerializeField] private VisitorOverlay _widget;
    [SerializeField] private Camera _camera;
    private Dictionary<VisitorCreature, VisitorOverlay> _overlays = new Dictionary<VisitorCreature, VisitorOverlay>();

    public void OnAwake()
    {
        //Clear children before game start.
        var childCount = _root.childCount;
        for (var i = _root.childCount - 1; i > 0; --i)
        {
            Destroy(_root.GetChild(i).gameObject);
        }
    }
    
    public void OnStart()
    {
        ObjectManager.instance.OnVisitorSpawned.AddListener(OnVisitorAdded);
    }
    
    public void OnVisitorAdded(VisitorCreature visitor)
    {
        var widget = Instantiate(_widget, _root);
        widget.SetVisitor(visitor, _camera);
        visitor.OnDespawn.AddListener(OnVisitorRemoved);
        _overlays.Add(visitor, widget);
    }

    public void OnVisitorRemoved(VisitorCreature visitor)
    {
        if (_overlays.TryGetValue(visitor, out var widget))
            Destroy(widget);
    }
}
