using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AiAction : ScriptableObject
{
    public abstract void OnActivate(Creature creature);
    public abstract void OnUpdate(Creature creature);
    public abstract void OnDeactivate(Creature creature);
}
