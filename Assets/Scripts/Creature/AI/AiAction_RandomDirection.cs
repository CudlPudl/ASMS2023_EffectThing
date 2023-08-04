using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "newAibehaviour", menuName = "Ai/Action: RandomDirection", order = 51)]
public class AiAction_RandomDirection : AiAction
{
    [SerializeField] private float movementSpeed = 1f;
    [SerializeField] private float durationPerDirection = 1f;
    [SerializeField] private float maxAngleChange = 15f;

    public override void OnActivate(Creature creature)
    {
        creature.Ai.AiFloats.Clear();
        creature.Ai.AiFloats.Add(0.0f); // index 0 = timer
        creature.Ai.AiFloats.Add(Random.Range(0f, 360f)); // index 1 = current angle

        creature.Ai.TargetType = CreatureAiTargetType.direction;
        SetDirection(creature);
    }

    public override void OnDeactivate(Creature creature)
    {
        creature.Ai.AiFloats.Clear();
    }

    public override void OnUpdate(Creature creature)
    {
        creature.Ai.AiFloats[0] += Time.deltaTime;
        if (creature.Ai.AiFloats[0] >= durationPerDirection)
        { NewDirection(creature); }

        creature.Movement.Move(creature.Ai.GetTargetDirection() * movementSpeed * Time.deltaTime);
    }

    private void NewDirection(Creature creature)
    {
        creature.Ai.AiFloats[0] = 0f;

        creature.Ai.AiFloats[1] += Random.Range(-maxAngleChange, maxAngleChange);
        if (creature.Ai.AiFloats[1] >= 360f) { creature.Ai.AiFloats[1] -= 360f; }
        else if (creature.Ai.AiFloats[1] < 0f) { creature.Ai.AiFloats[1] += 360f; }

        SetDirection(creature);
    }

    private void SetDirection(Creature creature)
    {
        Vector2 direction = creature.Ai.AiFloats[1].DegreeToVector2();
        creature.Ai.DirectionTarget = new Vector3(direction.x, 0f, direction.y);
    }
}
