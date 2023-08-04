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
        VisitorCreature visitor = other.transform.root.GetComponent<VisitorCreature>();
        if (visitor == null) { return; }
        if (visitor.VisitorAi.IsMad) { visitor.Ai.ActivateAction(despawnAction); }
    }
}
