using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class StaffSelectorWidget : MonoBehaviour
{
    [SerializeField] private StaffSelector _selector;
    [SerializeField] private StaffCreature _staff;
    [SerializeField] private GameObject _selectionGroup;

    public void Initialize(StaffCreature creature, StaffSelector selector)
    {
        _staff = creature;
        _selector = selector;
        _selector.OnStaffSelect.AddListener(OnStaffSelected);
    }
    
    public void OnStaffSelected(StaffCreature staff)
    {
        var isSelected = false;
        foreach (var s in _selector.SelectedStaff)
        {
            if (s == _staff)
                isSelected = true;
        }
        _selectionGroup.SetActive(isSelected);
    }

    public void OnWantedTypeChanged(VisitorType type)
    {
        _staff.WantedVisitorType = type;
    }

    public void OnClick()
    {
        _selector.SelectStaff(_staff);
    }
}
