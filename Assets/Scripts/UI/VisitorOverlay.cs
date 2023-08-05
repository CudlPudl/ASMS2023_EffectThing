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
    
    public void SetVisitor(VisitorCreature visitor)
    {
        _visitor = visitor;
        _followTarget = _visitor.transform;
        // ADD CAMERA REFERENCE HERE
        _icon.SetType(_visitor.VisitorType);
    }
}
