using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class StaffBubble : FollowWorldObject
{
    [SerializeField] private StaffSelectionButton _preferenceWidget;

    private StaffCreature _creature;
    public void Initialize(StaffCreature creature, Camera camera)
    {
        _followTarget = creature.transform;
        _camera = camera;
        _creature = creature;
        _creature.OnVisitorTypeChanged.AddListener(OnTypeChanged);
        OnTypeChanged(_creature.WantedVisitorType);
        Update();
    }

    private void OnTypeChanged(VisitorType type)
    {
        _preferenceWidget.SetType(type);    
        gameObject.SetGameObjectActive(type != VisitorType.none);
    }
}
