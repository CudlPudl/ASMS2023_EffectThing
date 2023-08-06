using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class StaffBubbleHandler : MonoBehaviour
{
    [SerializeField] private StaffBubble _prefab;
    [SerializeField] private Transform _root;
    [SerializeField] private Camera _camera;
    private StaffCreature _creature;
    private Dictionary<StaffCreature, StaffBubble> _bubbles = new Dictionary<StaffCreature, StaffBubble>();

    public void Start()
    {
        ObjectManager.instance.OnSpawnedStaff.AddListener(OnStaffAdded);
        ObjectManager.instance.OnDespawnedStaff.AddListener(OnStaffRemoved);
    }
    
    public void OnStaffAdded(StaffCreature creature)
    {
        var go = Instantiate(_prefab, _root);
        go.Initialize(creature, _camera);
        _bubbles.Add(creature, go);
    }

    public void OnStaffRemoved(StaffCreature creature)
    {
        if (_bubbles.TryGetValue(creature, out var bubble))
            Destroy(bubble.gameObject);
    }
}
