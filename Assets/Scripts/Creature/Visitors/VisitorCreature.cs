using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
[System.Serializable] public class VisitorEvent : UnityEvent<VisitorCreature> { }

public class VisitorCreature : Creature
{
    public VisitorEvent OnDespawn = new VisitorEvent();
    [SerializeField] private VisitorType visitorType = VisitorType.none;

    public VisitorAi VisitorAi => Ai as VisitorAi;

    public VisitorType VisitorType => visitorType;

    public VisitorCapturer CurrentlyCapturedBy { get; set; } = null;

    
    public override void Despawn()
    {
        if (CurrentlyCapturedBy != null) { CurrentlyCapturedBy.DeregisterVisitor(this); }
        ObjectManager.instance.SpawnedVisitors.Remove(this);
        base.Despawn();
    }
}

public enum VisitorType
{
    any = -1,
    none = 0,
    nerd = 1,
    geek = 2,
    gamer = 3,
}