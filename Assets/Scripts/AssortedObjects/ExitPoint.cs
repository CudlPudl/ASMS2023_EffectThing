using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitPoint : MonoBehaviour
{
    [SerializeField] private AiAction despawnAction;
    void Start()
    {
        ObjectManager.instance.ExitPoints.Add(this.gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        Creature creature = other.transform.root.GetComponent<Creature>();
        if (creature == null) { return; }
        if (creature.Ai.IsMad)
        {
            creature.Ai.ActivateAction(despawnAction);
            if (creature.CreatureType == CreatureType.visitor)
            { ScoreManager.instance.ReducePoint(); }
        }
    }
}
