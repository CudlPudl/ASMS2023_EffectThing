using System.Collections;
using System.Collections.Generic;
using Unity.PlasticSCM.Editor.WebApi;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class VisitorOverlay : FollowWorldObject
{
    private VisitorCreature _visitor;
    [SerializeField] private StaffSelectionButton _icon;
    [SerializeField] private Slider _happiness;
    [SerializeField] private CanvasGroup _canvasGroup;

    public void SetVisitor(VisitorCreature visitor, Camera camera)
    {
        _visitor = visitor;
        _followTarget = _visitor.transform;
        _camera = camera;
        _icon.SetType(_visitor.VisitorType);
        Update();
    }
    
    protected override void Update()
    {
        var compensation = 1080f / Screen.height;
        var distance = Vector2.Distance(Input.mousePosition, transform.position);
        _canvasGroup.alpha = Mathf.Clamp01(1 - (distance - 150) / 50 * compensation);
        base.Update();
    }
}
