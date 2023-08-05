using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class StaffSelectorWidget : MonoBehaviour
{
    [SerializeField] private StaffSelector _selector;
    [SerializeField] private StaffCreature _staff;
    [SerializeField] private GameObject _selectionGroup;
    [SerializeField] private WidgetSwitcher _switcher;
    
    public void Initialize(StaffCreature creature, StaffSelector selector)
    {
        _staff = creature;
        _selector = selector;
    }
    
    public void OnStaffSelected(StaffCreature staff)
    {
        _selectionGroup.SetActive(staff = _staff);
    }

    public void OnWantedTypeChanged(VisitorType type)
    {
        _staff.WantedVisitorType = type;
        //Handle Toggle Logic here.
        var index = 0;
        switch (type)
        {
            case VisitorType.any:
                _switcher.SetActiveIndex(0);
                break;
            case VisitorType.nerd:
                _switcher.SetActiveIndex(1);
                break;
            case VisitorType.geek:
                _switcher.SetActiveIndex(2);
                break;
            case VisitorType.gamer:
                _switcher.SetActiveIndex(3);
                break;
            case VisitorType.none:
                _switcher.SetActiveIndex(4);
                break;
        }
    }
}
