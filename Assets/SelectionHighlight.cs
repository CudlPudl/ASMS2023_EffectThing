using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class SelectionHighlighter : MonoBehaviour
{
    [SerializeField] private StaffSelector _selector;
    [SerializeField] private GameObject SelectionHighlight;
    private Dictionary<StaffCreature, Transform> _highlights = new Dictionary<StaffCreature, Transform>();

    public void Start()
    {
        ObjectManager.instance.OnSpawnedStaff.AddListener(OnStaffCreated);
        ObjectManager.instance.OnDespawnedStaff.AddListener(OnStaffDespawned);
        _selector.OnStaffSelect.AddListener(OnStaffSelected);
    }

    public void OnStaffCreated(StaffCreature staff)
    {
        var t = Instantiate(SelectionHighlight).transform;
        t.position = staff.transform.position;
        t.gameObject.SetActive(false);
        _highlights.Add(staff, t);
    }

    public void OnStaffSelected(StaffCreature staff)
    {
        var highlights = _highlights.Keys;
        foreach (var s in highlights)
        {
            var isSelected = false;
            foreach (var creature in _selector.SelectedStaff)
            {
                if (s == creature)
                {
                    isSelected = true;
                    break;
                }
            }
            _highlights[s].gameObject.SetGameObjectActive(isSelected);
        }
    }

    public void OnStaffDespawned(StaffCreature staff) { _highlights.Remove(staff); }

    public void Update()
    {
        foreach (var staff in _highlights.Keys)
        {
            _highlights[staff].position = staff.transform.position;
        }
    }
}
