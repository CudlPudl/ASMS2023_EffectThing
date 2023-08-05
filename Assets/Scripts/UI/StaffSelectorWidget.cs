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
    }
    
    public void OnStaffSelected(StaffCreature staff)
    {
        _selectionGroup.SetActive(staff = _staff);
    }

    public void OnWantedTypeChanged(VisitorType type)
    {
        
    }
}
