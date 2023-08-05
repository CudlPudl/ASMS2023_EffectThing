using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class StaffSelectorWidget : MonoBehaviour
{
    [SerializeField] private StaffSelector _selector;
    [SerializeField] private StaffCreature _staff;
    [SerializeField] private GameObject _selectionGroup;
    [SerializeField] private CameraMover _cameraMover;

    public void Initialize(StaffCreature creature, StaffSelector selector, CameraMover cameraMover)
    {
        _staff = creature;
        _selector = selector;
        _cameraMover = cameraMover;
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
        _selector.RecordPreviousSelectionTime();
    }

    public void OnClick()
    {
        _selector.SelectStaff(_staff);
        _cameraMover.SetTargetTo(_staff.gameObject);
    }
}
