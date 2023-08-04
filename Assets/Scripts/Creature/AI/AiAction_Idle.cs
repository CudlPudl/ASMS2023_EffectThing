using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "newAibehaviour", menuName = "Ai/Action: Idle", order = 10)]
public class AiAction_Idle : AiAction
{
    [Tooltip("Set to -1 to idle forever.")]
    [SerializeField] private float duration = -1f;
    [SerializeField] private GoalAction onIdleEnd;

    public override void OnActivate(Creature creature)
    {
        creature.Ai.AiFloats.Clear();
        creature.Ai.AiFloats.Add(0f);
    }

    public override void OnDeactivate(Creature creature)
    {
        creature.Ai.AiFloats.Clear();
    }

    public override void OnUpdate(Creature creature)
    {
        if (duration <= 0f) { return; }
        creature.Ai.AiFloats[0] += Time.deltaTime;
        if (creature.Ai.AiFloats[0] >= duration)
        { onIdleEnd.SetAction(creature); }
    }
}
