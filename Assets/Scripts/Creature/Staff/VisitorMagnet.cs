using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VisitorMagnet : VisitorCapturer
{
    [SerializeField] private VisitorType visitorType = VisitorType.none;
    [SerializeField] private GameObject visitorTargetObject;
    [SerializeField] private AiAction onCaptureAction;

    [Space(20)]
    [SerializeField] private SpriteRenderer bubbleRend;
    [SerializeField] private SpriteRenderer iconRend;
    [SerializeField] private Sprite nerdIcon;
    [SerializeField] private Sprite geekIcon;
    [SerializeField] private Sprite gamerIcon;
    [SerializeField] private Sprite anyIcon;

    public VisitorType VisitorType
    {
        get => visitorType; set
        {
            visitorType = value;
            switch (visitorType)
            {
                case VisitorType.none:
                    bubbleRend.enabled = false;
                    iconRend.enabled = false;
                    return;
                case VisitorType.any:
                    iconRend.sprite = anyIcon;
                    break;
                case VisitorType.nerd:
                    iconRend.sprite = nerdIcon;
                    break;
                case VisitorType.geek:
                    iconRend.sprite = geekIcon;
                    break;
                case VisitorType.gamer:
                    iconRend.sprite = gamerIcon;
                    break;
            }
            iconRend.enabled = true;
            bubbleRend.enabled = true;
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
