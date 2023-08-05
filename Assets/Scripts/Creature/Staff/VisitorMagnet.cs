using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VisitorMagnet : VisitorCapturer
{
    [SerializeField] private VisitorType visitorType = VisitorType.none;
    [SerializeField] private GameObject visitorTargetObject;
    [SerializeField] private AiAction onCaptureAction;

    [Space(20)]
    [SerializeField] private MeshRenderer tempRend;
    [SerializeField] private Material nerdMat;
    [SerializeField] private Material geekMat;
    [SerializeField] private Material gamerMat;
    [SerializeField] private Material noneMat;

    public VisitorType VisitorType
    {
        get => visitorType; set
        {
            visitorType = value;
            switch (visitorType)
            {
                case VisitorType.any:
                case VisitorType.none:
                    tempRend.material = noneMat;
                    break;
                case VisitorType.nerd:
                    tempRend.material = nerdMat;
                    break;
                case VisitorType.geek:
                    tempRend.material = geekMat;
                    break;
                case VisitorType.gamer:
                    tempRend.material = gamerMat;
                    break;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (VisitorType == VisitorType.none) { return; }

        VisitorCreature visitor = other.GetComponent<VisitorCreature>();
        if (visitor == null) { return; }
        if (!visitor.VisitorAi.IsCapturable()) { return; }
        if (VisitorType == VisitorType.any || VisitorType == visitor.VisitorType)
        {
            visitor.Ai.ObjectTarget = visitorTargetObject;
            visitor.Ai.TargetType = CreatureAiTargetType.gameObject;
            visitor.Ai.ActivateAction(onCaptureAction);
            RegisterVisitor(visitor);
        }
    }
}
