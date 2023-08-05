using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public abstract class CreatureAI : MonoBehaviour
{
    [SerializeField] private Creature creature;
    [Space(20)]
    [SerializeField] private AiAction defaultOnSpawnAction;
    [SerializeField] private AiAction defaultOnTargetSetAction;



    // Actions
    public AiAction DefaultOnSpawnAction => defaultOnSpawnAction;
    public AiAction DefaultOnTargetSetAction => defaultOnTargetSetAction;

    public AiAction OnSpawnAction { get; set; }
    public AiAction OnTargetSetAction { get; set; }



    // Things
    public CreatureAiTargetType TargetType { get; set; } = CreatureAiTargetType.none;
    public AiAction ActiveAction { get; set; } = null;
    public Vector3 LocationTarget { get; set; } = Vector3.zero;
    public GameObject ObjectTarget { get; set; } = null;
    public Vector3 DirectionTarget { get; set; } = Vector3.zero;
    public bool IsMad { get; protected set; } = false;
    public Creature Creature => creature;



    // Values for AiActions to control
    public List<bool> AiBools { get; set; } = new List<bool>();
    public List<int> AiInts { get; set; } = new List<int>();
    public List<float> AiFloats { get; set; } = new List<float>();
    public List<Vector3> AiVector3s { get; set; } = new List<Vector3>();



    // Awake / Start
    protected virtual void Awake()
    {
        OnSpawnAction = DefaultOnSpawnAction;
        OnTargetSetAction = DefaultOnTargetSetAction;
    }

    protected virtual void Start()
    { ActivateAction(OnSpawnAction); }




    // Update
    protected virtual void Update()
    {
        ActiveAction.OnUpdate(creature);
    }




    // Target
    public void SetTargetLocation(Vector3 location)
    {
        LocationTarget = location;
        TargetType = CreatureAiTargetType.location;
        OnTargetChange();
    }
    public void SetTargetObject(GameObject gameObject)
    {
        ObjectTarget = gameObject;
        TargetType = CreatureAiTargetType.gameObject;
        OnTargetChange();
    }
    public void SetTargetDirection(Vector3 direction)
    {
        DirectionTarget = direction;
        TargetType = CreatureAiTargetType.direction;
        OnTargetChange();
    }
    public void SetTargetToNone()
    {
        TargetType = CreatureAiTargetType.none;
        OnTargetChange();
    }
    protected virtual void OnTargetChange()
    { ActivateAction(OnTargetSetAction); }



    // Activate Action
    public void ActivateAction(AiAction action)
    {
        if (action == null) { return; }
        if (ActiveAction != null) { ActiveAction.OnDeactivate(creature); }
        ActiveAction = action;
        ActiveAction.OnActivate(creature);
    }
    public abstract void ActivateAction(CreatureAiBaseAction actionType);



    // Methods
    public Vector3 GetTargetLocation()
    {
        switch (TargetType)
        {
            case CreatureAiTargetType.none:
                return transform.position;
            case CreatureAiTargetType.location:
                return LocationTarget;
            case CreatureAiTargetType.gameObject:
                if (ObjectTarget != null) { return ObjectTarget.transform.position; }
                else { return transform.position; }
            case CreatureAiTargetType.direction:
                return transform.position += DirectionTarget;
        }
        return transform.position;
    }
    public Vector3 GetTargetDirection()
    {
        switch (TargetType)
        {
            case CreatureAiTargetType.none:
                return Vector3.zero;
            case CreatureAiTargetType.location:
                if (LocationTarget == Vector3.zero) { return Vector3.zero; }
                return (new Vector3(LocationTarget.x, 0f, LocationTarget.z)
                    - new Vector3(transform.position.x, 0f, transform.position.z)).normalized;
            case CreatureAiTargetType.gameObject:
                if (ObjectTarget == null) { return Vector3.zero; }
                return (new Vector3(ObjectTarget.transform.position.x, 0f, ObjectTarget.transform.position.z)
                    - new Vector3(transform.position.x, 0f, transform.position.z)).normalized;
            case CreatureAiTargetType.direction:
                return DirectionTarget;
        }
        return Vector3.zero;
    }
    public float DistanceToTarget()
    {
        Vector3 loc = GetTargetLocation();
        return Vector3.Distance(new Vector3(transform.position.x, 0f, transform.position.z), new Vector3(loc.x, 0f, loc.z));
    }
}


public enum CreatureAiTargetType
{
    none = 0,

    location = 1,
    gameObject = 2,
    direction = 3,
}

public enum CreatureAiBaseAction
{
    none = 0,

    defaultSpawnAction = 1,
    defaultSelectAction = 2,
    defaultTargetSetAction = 4,
    defaultBoothActivityEnd = 5,

    spawnAction = 11,
    selectAction = 12,
    targetSetAction = 14,
    boothActivityEnd = 15,
}