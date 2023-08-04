using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "newAibehaviour", menuName = "Ai/Action: Despawn", order = 71)]
public class AiAction_Despawn : AiAction
{
    [SerializeField] private float despawnTime = 1.0f;
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
        creature.Ai.AiFloats[0] += Time.deltaTime;
        if (creature.Ai.AiFloats[0] >= despawnTime)
        {
            creature.Despawn();
        }
    }
}
