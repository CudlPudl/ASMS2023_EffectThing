using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "newAibehaviour", menuName = "Ai/Action: GoToExit", order = 70)]
public class AiAction_GoToExit : AiAction
{
    [SerializeField] private AiAction movementActionToExit;
    public override void OnActivate(Creature creature)
    {
        creature.Ai.ObjectTarget = ObjectManager.instance.ExitPoints[Random.Range(0, ObjectManager.instance.ExitPoints.Count)];
        creature.Ai.TargetType = CreatureAiTargetType.gameObject;
        creature.Ai.ActivateAction(movementActionToExit);
    
    }

    public override void OnDeactivate(Creature creature)
    {
    }

    public override void OnUpdate(Creature creature)
    {
    }
}