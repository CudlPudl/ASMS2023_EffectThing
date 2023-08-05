using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEngine;

public class VisitorOverlayHandler : MonoBehaviour
{
    [SerializeField] private Transform _root;
    [SerializeField] private VisitorOverlay _widget;
    private Dictionary<VisitorCreature, VisitorOverlay> _overlays = new Dictionary<VisitorCreature, VisitorOverlay>();
    
    public void OnVisitorAdded(VisitorCreature visitor)
    {
        var widget = Instantiate(_widget, _root);
        widget.SetVisitor(visitor);
        _overlays.Add(visitor, widget);
    }

    public void OnVisitorRemoved(VisitorCreature visitor)
    {
        if (_overlays.TryGetValue(visitor, out var widget))
            Destroy(widget);
    }
}
