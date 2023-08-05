using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventBooth : VisitorCapturer
{
    [SerializeField] private GoalAction onBoothAction;
    [SerializeField] private GoalAction onBoothEndAction;
    [SerializeField] private Q4MaterialColor monitorColorPlayer;
    [SerializeField] private Q4VarColor monitorOffColor;
    public BoothActivity BoothActivity { get; set; } = new BoothActivity();

    public bool IsOn { get; set; } = false;
    private float Timer = 0.0f;

    void Start()
    {
        ObjectManager.instance.InactiveEventBooths.Add(this);
    }

    private void OnTriggerEnter(Collider other)
    {
        VisitorCreature visitor = other.transform.root.GetComponent<VisitorCreature>();
        if (visitor == null) { return; }

        if (IsOn && visitor.VisitorAi.IsCapturable())
        {
            visitor.VisitorAi.CurrentActivity = BoothActivity.BoothActivityType;
            onBoothAction.SetAction(visitor);
            RegisterVisitor(visitor);
        }
        else
        {
            visitor.VisitorAi.CurrentActivity = VisitorType.none;
            onBoothEndAction.SetAction(visitor);
            DeregisterVisitor(visitor);
        }
    }

    private void Update()
    {
        Timer += Time.deltaTime;
        if (Timer >= BoothActivity.Duration)
        { EndActivity(); }
    }

    public void SetActivity()
    {
        if (IsOn || ObjectManager.instance.AvailableBoothActivities.Count == 0) { return; }

        BoothActivity = ObjectManager.instance.AvailableBoothActivities[0];
        ObjectManager.instance.AvailableBoothActivities.RemoveAt(0);

        ObjectManager.instance.InactiveEventBooths.Remove(this);

        monitorColorPlayer.ReplacePreset(BoothActivity.MonitorColor);

        Timer = 0.0f;
        IsOn = true;
    }
    private void EndActivity()
    {
        IsOn = false;
        Timer = 0.0f;

        ObjectManager.instance.ActiveEventBooths.Remove(this);
        ObjectManager.instance.InactiveEventBooths.Add(this);

        ObjectManager.instance.AvailableBoothActivities.Add(BoothActivity);
        BoothActivity = new BoothActivity();

        this.enabled = false;
        this.enabled = true;

        monitorColorPlayer.ReplacePreset(monitorOffColor);
    }
}

[System.Serializable]
public class BoothActivity
{
    public BoothActivity()
    {
        boothActivityType = VisitorType.none;
        duration = 9999999f;
    }

    [SerializeField] private VisitorType boothActivityType = VisitorType.none;
    [SerializeField] private float duration = 9999999f;
    [SerializeField] private Q4VarColor monitorColor;

    public VisitorType BoothActivityType => boothActivityType;
    public float Duration => duration;
    public Q4VarColor MonitorColor => monitorColor;
}