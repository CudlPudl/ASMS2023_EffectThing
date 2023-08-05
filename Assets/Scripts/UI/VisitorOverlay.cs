using System.Collections;
using System.Collections.Generic;
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
}
