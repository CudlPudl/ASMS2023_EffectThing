using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
[System.Serializable] public class VisitorEvent : UnityEvent<VisitorCreature> { }

public class VisitorCreature : Creature
{
    [SerializeField] private VisitorType visitorType = VisitorType.none;


    public VisitorType VisitorType => visitorType;



}

public enum VisitorType
{
    any = -1,
    none = 0,
    nerd = 1,
    geek = 2,
    gamer = 3,
}