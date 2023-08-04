using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventBooth : MonoBehaviour
{
    [SerializeField] private GoalAction onBoothAction;
    [SerializeField] private GoalAction onBoothEndAction;
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

        if (IsOn)
        {
            visitor.VisitorAi.CurrentActivity = BoothActivity.BoothActivityType;
            onBoothAction.SetAction(visitor);
        }
        else
        {
            visitor.VisitorAi.CurrentActivity = VisitorType.none;
            onBoothEndAction.SetAction(visitor);
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

        Timer = 0.0f;
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

    public VisitorType BoothActivityType => boothActivityType;
    public float Duration => duration;
}