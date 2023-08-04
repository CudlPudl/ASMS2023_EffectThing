using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VisitorAi : CreatureAI
{
    [SerializeField] private VisitorType currentActivity = VisitorType.none;
    [SerializeField] private float startPatience = 100f;
    [SerializeField] private List<VisitorPatienceStat> patienceStats = new List<VisitorPatienceStat>();
    [SerializeField] private AiAction defaultOnZeroPatience;
    [SerializeField] private AiAction defaultOnBoothActivityEnd;

    public AiAction DefaultOnZeroPatience => defaultOnZeroPatience;
    public AiAction DefaultOnBoothActivityEnd => defaultOnBoothActivityEnd;
    public AiAction OnZeroPatience { get; set; }
    public AiAction OnBoothActivityEnd { get; set; }


    private float patienceModifier = 0.0f;
    public float CurrentPatience { get; private set; } = 100f;
    public bool IsMad { get; private set; } = false;

    public void AddPatience(float amount)
    {
        CurrentPatience += amount;
        if (!IsMad && CurrentPatience <= 0f)
        {
            IsMad = true;
            ActivateAction(OnZeroPatience);
        }
        else if (IsMad && CurrentPatience > 0f)
        { IsMad = false; }
    }

    public VisitorType CurrentActivity
    {
        get => currentActivity;
        set
        {
            if (currentActivity == value) { return; }
            currentActivity = value;
            CalculateCurrentPatienceModifier();
        }
    }

    protected override void Awake()
    {
        OnZeroPatience = DefaultOnZeroPatience;
        OnBoothActivityEnd = DefaultOnBoothActivityEnd;
        base.Awake();
    }

    protected override void Start()
    {
        CurrentPatience = startPatience;
        IsMad = false;
        CalculateCurrentPatienceModifier();
        base.Start();
    }

    protected override void Update()
    {
        AddPatience(patienceModifier * Time.deltaTime);
        base.Update();
    }

    private void CalculateCurrentPatienceModifier()
    {
        patienceModifier = 0.0f;
        patienceStats.ForEach(x =>
        {
            if (CurrentActivity == x.EntertainmentType)
            { patienceModifier += x.PatiencePerSecond; return; }

            if (x.EntertainmentType == VisitorType.any && CurrentActivity != VisitorType.none)
            { patienceModifier += x.PatiencePerSecond; return; }

            if (CurrentActivity == VisitorType.any && x.EntertainmentType != VisitorType.none)
            { patienceModifier += x.PatiencePerSecond; return; }
        });
    }


    public override void ActivateAction(CreatureAiBaseAction actionType)
    {
        switch (actionType)
        {
            case CreatureAiBaseAction.none:
                break;
            case CreatureAiBaseAction.defaultSpawnAction:
                ActivateAction(DefaultOnSpawnAction);
                break;
            case CreatureAiBaseAction.defaultSelectAction:
                ActivateAction(DefaultOnSpawnAction);
                break;
            case CreatureAiBaseAction.defaultTargetSetAction:
                ActivateAction(DefaultOnTargetSetAction);
                break;
            case CreatureAiBaseAction.spawnAction:
                ActivateAction(OnSpawnAction);
                break;
            case CreatureAiBaseAction.selectAction:
                ActivateAction(DefaultOnSpawnAction);
                break;
            case CreatureAiBaseAction.targetSetAction:
                ActivateAction(OnTargetSetAction);
                break;
        }
    }
}


[System.Serializable]
public class VisitorPatienceStat
{
    [SerializeField] private VisitorType entertainmentType = VisitorType.none;
    [SerializeField] private float patiencePerSecond = -1f;


    public VisitorType EntertainmentType => entertainmentType;
    public float PatiencePerSecond => patiencePerSecond;
}