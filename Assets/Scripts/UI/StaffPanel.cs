using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class StaffPanel : MonoBehaviour
{
    [SerializeField] private Transform _root;
    [SerializeField] private StaffSelectorWidget _widget;
    [SerializeField] private StaffSelector _staffSelector;
    
    public void OnAwake()
    {
        ObjectManager.instance.OnSpawnedStaff.AddListener(OnStaffSpawned);
    }

    private void OnStaffSpawned(List<StaffCreature> staff)
    {
        var childCount = _root.childCount;
        for (var i = childCount - 1; i < 0; --i)
        {
            Destroy(_root.GetChild(i).gameObject);
        }

        foreach (var member in staff)
        {
            var go = Instantiate(_widget, _root, true);
            _widget.Initialize(member, _staffSelector);
        }
    }
    

}
