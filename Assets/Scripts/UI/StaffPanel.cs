using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class StaffPanel : MonoBehaviour
{
    [SerializeField] private Transform _root;
    [SerializeField] private StaffSelectorWidget _widget;
    [SerializeField] private StaffSelector _staffSelector;
    [SerializeField] private CameraMover _cameraMover;


    public void Start()
    {
        ScoreManager.instance.onGameLost.AddListener(OnLost);
        OnLost();

        ObjectManager.instance.OnSpawnedStaff.AddListener(OnStaffSpawned);
        ObjectManager.instance.SpawnedStaffs.ForEach(x => OnStaffSpawned(x));
    }
    public void OnLost()
    {
        var childCount = _root.childCount;
        if (childCount == 0)
            return;
        for (var i = childCount - 1; i >= 0; i--)
        {
            Destroy(_root.GetChild(i).gameObject);
        }
    }

    private void OnStaffSpawned(StaffCreature staff)
    {
        var go = Instantiate(_widget, _root);
        go.Initialize(staff, _staffSelector, _cameraMover);
        go.OnStaffSelected(null);
    }
}
