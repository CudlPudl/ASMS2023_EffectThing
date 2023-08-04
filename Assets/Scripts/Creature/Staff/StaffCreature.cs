using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
[System.Serializable] public class StaffEvent : UnityEvent<StaffCreature> { }

public class StaffCreature : Creature
{
    [SerializeField] private VisitorType wantedVisitorType = VisitorType.none;
    [SerializeField] private VisitorMagnet visitorMagnet;

    public StaffAi StaffAi => Ai as StaffAi;

    public VisitorType WantedVisitorType { get => wantedVisitorType; set { wantedVisitorType = value; visitorMagnet.VisitorType = value; } }




}