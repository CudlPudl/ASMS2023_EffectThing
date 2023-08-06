using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class StaffSelectorWidget : MonoBehaviour
{
    [SerializeField] private StaffSelector _selector;
    [SerializeField] private StaffCreature _staff;
    [SerializeField] private GameObject _selectionGroup;
    [SerializeField] private CameraMover _cameraMover;
    [SerializeField] private TMP_Text _text;
    [SerializeField] private List<string> _names;

    public StaffCreature Staff => _staff;

    public void Initialize(StaffCreature creature, StaffSelector selector, CameraMover cameraMover)
    {
        _staff = creature;
        _selector = selector;
        _cameraMover = cameraMover;
        var index = transform.GetSiblingIndex();
        _text.text = _names[(int)Mathf.Repeat(index + Random.Range(0, _names.Count - 1), _names.Count )];
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
