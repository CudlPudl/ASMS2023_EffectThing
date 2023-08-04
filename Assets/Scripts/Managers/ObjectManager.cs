using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectManager : MonoBehaviour
{
    public static ObjectManager instance;
    [SerializeField] private List<BoothActivity> availableBoothActivities = new List<BoothActivity>();
    [SerializeField] private List<float> boothActivitySpawnDelays = new List<float>();
    public float RandomBoothSpawnDelay => boothActivitySpawnDelays[Random.Range(0, boothActivitySpawnDelays.Count)];

    public List<GameObject> ExitPoints { get; set; } = new List<GameObject>();
    public List<EventBooth> InactiveEventBooths { get; set; } = new List<EventBooth>();
    public List<EventBooth> ActiveEventBooths { get; set; } = new List<EventBooth>();
    public List<BoothActivity> AvailableBoothActivities
    { get => availableBoothActivities; set => availableBoothActivities = value; }

    private void Awake()
    {
        if (instance == null) { instance = this; }
        else if (instance != this) { Destroy(gameObject); }
    }

    private void Update()
    {
        BoothSpawnerUpdate();
    }

    private float boothSpawnerTimer = 0.0f;
    private void BoothSpawnerUpdate()
    {
        boothSpawnerTimer -= Time.deltaTime;
        if (boothSpawnerTimer > 0f) { return; }

        boothSpawnerTimer = RandomBoothSpawnDelay;

        if (InactiveEventBooths.Count == 0 || AvailableBoothActivities.Count == 0) { return; }
        InactiveEventBooths[Random.Range(0, InactiveEventBooths.Count)].SetActivity();
    }
}
