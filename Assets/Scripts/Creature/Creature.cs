using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
[System.Serializable] public class CreatureEvent : UnityEvent<Creature> { }

public abstract class Creature : MonoBehaviour
{
    [SerializeField] private CreatureType creatureType = CreatureType.visitor;
    [SerializeField] private CreatureMovement movement;
    [SerializeField] private CreatureAI ai;


    public CreatureType CreatureType => creatureType;
    public CreatureMovement Movement => movement;
    public CreatureAI Ai => ai;
}

public enum CreatureType
{
    visitor = 1,
    staff = 2,
}