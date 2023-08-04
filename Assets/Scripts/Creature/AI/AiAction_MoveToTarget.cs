using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "newAibehaviour", menuName = "Ai/Action: MoveToTarget", order = 50)]
public class AiAction_MoveToTarget : AiAction
{
    [SerializeField] private float movementSpeed = 1f;
    [SerializeField] private float reachDistance = 1f;
    [SerializeField] private GoalAction onReachAction;

    public override void OnActivate(Creature creature)
    {
    }

    public override void OnDeactivate(Creature creature)
    {
    }

    public override void OnUpdate(Creature creature)
    {
        if (creature.Ai.DistanceToTarget() <= reachDistance)
        { OnReach(creature); return; }

        creature.Movement.Move(creature.Ai.GetTargetDirection() * movementSpeed * Time.deltaTime);
    }

    private void OnReach(Creature creature)
    {
        onReachAction.SetAction(creature);
    }
}
