using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VisitorMagnet : VisitorCapturer
{
    [SerializeField] private VisitorType visitorType = VisitorType.none;
    [SerializeField] private GameObject visitorTargetObject;
    [SerializeField] private AiAction onCaptureAction;

    public VisitorType VisitorType { get => visitorType; set => visitorType = value; }

    private void OnTriggerEnter(Collider other)
    {
        if (VisitorType == VisitorType.none) { return; }

        VisitorCreature visitor = other.GetComponent<VisitorCreature>();
        if (visitor == null) { return; }

        if (VisitorType == VisitorType.any || VisitorType == visitor.VisitorType)
        {
            visitor.Ai.ObjectTarget = visitorTargetObject;
            visitor.Ai.TargetType = CreatureAiTargetType.gameObject;
            visitor.Ai.ActivateAction(onCaptureAction);
            RegisterVisitor(visitor);
        }
    }
}
