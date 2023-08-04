using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CreatureAI : MonoBehaviour
{
    [SerializeField] private Creature creature;
    [Space(20)]
    [SerializeField] private AiAction defaultOnSpawnAction;
    [SerializeField] private AiAction defaultOnSelectAction;
    [SerializeField] private AiAction defaultOnFreeAction;
    [SerializeField] private AiAction defaultOnTargetSetAction;



    // Actions
    public AiAction OnSpawnAction { get; set; }
    public AiAction OnSelectAction { get; set; }
    public AiAction OnFreeAction { get; set; }
    public AiAction OnTargetSetAction { get; set; }



    // Things
    public CreatureAiTargetType TargetType { get; set; } = CreatureAiTargetType.none;
    public AiAction ActiveAction { get; set; } = null;
    public Vector3 LocationTarget { get; set; } = Vector3.zero;
    public GameObject ObjectTarget { get; set; } = null;
    public Vector3 DirectionTarget { get; set; } = Vector3.zero;



    // Values for AiActions to control
    public List<bool> AiBools { get; set; } = new List<bool>();
    public List<int> AiInts { get; set; } = new List<int>();
    public List<float> AiFloats { get; set; } = new List<float>();
    public List<Vector3> AiVector3s { get; set; } = new List<Vector3>();



    // Awake / Start
    protected void Awake()
    {
        OnSpawnAction = defaultOnSpawnAction;
        OnSelectAction = defaultOnSelectAction;
        OnFreeAction = defaultOnFreeAction;
        OnTargetSetAction = defaultOnTargetSetAction;
    }

    protected void Start()
    { ActivateAction(OnSpawnAction); }



    // Update
    private void Update()
    {
        ActiveAction.OnUpdate(creature);
    }



    // Select
    public void Select()
    {
        TargetType = CreatureAiTargetType.none;
        OnSelect();
    }
    protected void OnSelect() { ActivateAction(OnSelectAction); }



    // Free
    public void FreeAi()
    {
        TargetType = CreatureAiTargetType.none;
        OnFreeAi();
    }
    protected void OnFreeAi() { ActivateAction(OnFreeAction); }



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
    protected void OnTargetChange()
    { ActivateAction(OnTargetSetAction); }



    // Activate Action
    public void ActivateAction(AiAction action)
    {
        if (action == null) { return; }
        if (ActiveAction != null) { ActiveAction.OnDeactivate(creature); }
        ActiveAction = action;
        ActiveAction.OnActivate(creature);
    }
    public void ActivateAction(CreatureAiBaseAction actionType)
    {
        switch (actionType)
        {
            case CreatureAiBaseAction.none:
                break;
            case CreatureAiBaseAction.defaultSpawnAction:
                ActivateAction(defaultOnSpawnAction);
                break;
            case CreatureAiBaseAction.defaultSelectAction:
                ActivateAction(defaultOnSelectAction);
                break;
            case CreatureAiBaseAction.defaultFreeAction:
                ActivateAction(defaultOnFreeAction);
                break;
            case CreatureAiBaseAction.defaultTargetSetAction:
                ActivateAction(defaultOnFreeAction);
                break;
            case CreatureAiBaseAction.spawnAction:
                ActivateAction(OnSpawnAction);
                break;
            case CreatureAiBaseAction.selectAction:
                ActivateAction(OnSelectAction);
                break;
            case CreatureAiBaseAction.freeAction:
                ActivateAction(OnFreeAction);
                break;
            case CreatureAiBaseAction.targetSetAction:
                ActivateAction(OnTargetSetAction);
                break;
        }
    }



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
    defaultFreeAction = 3,
    defaultTargetSetAction = 4,

    spawnAction = 11,
    selectAction = 12,
    freeAction = 13,
    targetSetAction = 14
}