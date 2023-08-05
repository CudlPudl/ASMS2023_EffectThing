using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "newAibehaviour", menuName = "Ai/Action: MoveCloseToTarget", order = 49)]
public class AiAction_MoveCloseToTargetObject : AiAction
{
    [SerializeField] private float movementSpeed = 1f;
    [SerializeField] private float reachDistance = 1f;
    [SerializeField] private Vector2 offsetMagnitudeRange = new Vector2(1f, 3f);
    [SerializeField] private GoalAction onReachAction;

    public override void OnActivate(Creature creature)
    {
       // creature.Ai.TargetType = CreatureAiTargetType.gameObject;
        creature.Ai.AiVector3s.Clear();
        creature.Ai.AiVector3s.Add((
            new Vector3(Random.Range(-1f, 1f), 0.0f, Random.Range(-1f, 1f)).normalized
            ) * Random.Range(offsetMagnitudeRange.x, offsetMagnitudeRange.y));
    }

    public override void OnDeactivate(Creature creature)
    {
        creature.Ai.AiVector3s.Clear();
    }

    public override void OnUpdate(Creature creature)
    {
        if (GetDistanceToTarget(creature) <= reachDistance)
        { OnReach(creature); return; }

        creature.Movement.Move(GetTargetDirection(creature) * movementSpeed * Time.deltaTime);
    }

    public Vector3 GetTargetDirection(Creature creature)
    {
        return (new Vector3(creature.Ai.GetTargetLocation().x + creature.Ai.AiVector3s[0].x
            , 0f, creature.Ai.GetTargetLocation().z + creature.Ai.AiVector3s[0].z)
                    - new Vector3(creature.transform.position.x, 0f, creature.transform.position.z)).normalized;
    }
    public float GetDistanceToTarget(Creature creature)
    {
        Vector3 loc = creature.Ai.GetTargetLocation() + creature.Ai.AiVector3s[0];
        return Vector3.Distance(new Vector3(creature.transform.position.x, 0f, creature.transform.position.z), new Vector3(loc.x, 0f, loc.z));
    }

    private void OnReach(Creature creature)
    {
        onReachAction.SetAction(creature);
    }
}
