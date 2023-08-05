using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

public class ObjectManager : MonoBehaviour
{
    public static ObjectManager instance;

    public UnityEvent<List<StaffCreature>> OnSpawnedStaff;
    public UnityEvent<VisitorCreature> OnVisitorSpawned;
    
    [SerializeField] private float visitorSpawnDelay = 1f;
    [SerializeField] private List<VisitorCreature> visitorPrefabs = new List<VisitorCreature>();
    [SerializeField] private List<StaffCreature> staffPrefabs = new List<StaffCreature>();


    [Space(20)]
    [SerializeField] private List<BoothActivity> availableBoothActivities = new List<BoothActivity>();
    [SerializeField] private List<float> boothActivitySpawnDelays = new List<float>();
    public float RandomBoothSpawnDelay => boothActivitySpawnDelays[Random.Range(0, boothActivitySpawnDelays.Count)];



    public List<GameObject> ExitPoints { get; set; } = new List<GameObject>();
    public List<EventBooth> InactiveEventBooths { get; set; } = new List<EventBooth>();
    public List<EventBooth> ActiveEventBooths { get; set; } = new List<EventBooth>();
    public List<BoothActivity> AvailableBoothActivities
    { get => availableBoothActivities; set => availableBoothActivities = value; }


    public List<StaffCreature> SpawnedStaffs { get; set; } = new List<StaffCreature>();
    public List<VisitorCreature> SpawnedVisitors { get; set; } = new List<VisitorCreature>();


    // Setup
    private void Awake()
    {
        if (instance == null) { instance = this; }
        else if (instance != this) { Destroy(gameObject); }
    }

    private void Start()
    {
        boothSpawnerTimer = RandomBoothSpawnDelay;
    }



    // Updates
    private void Update()
    {
        BoothSpawnerUpdate();
        VisitorSpawnerUpdate();
    }


    private float boothSpawnerTimer = 0.0f;
    private void BoothSpawnerUpdate()
    {
        boothSpawnerTimer -= Time.deltaTime;
        if (boothSpawnerTimer > 0f) { return; }

        boothSpawnerTimer = RandomBoothSpawnDelay;
    }


    private float visitorSpawnerTimer = 0.0f;
    private void VisitorSpawnerUpdate()
    {
        visitorSpawnerTimer -= Time.deltaTime;
        if (visitorSpawnerTimer > 0f) { return; }

        visitorSpawnerTimer = visitorSpawnDelay;

        SpawnVisitor();
    }




    // Spawns
    public void SpawnBooth()
    {
        if (InactiveEventBooths.Count == 0 || AvailableBoothActivities.Count == 0) { return; }
        InactiveEventBooths[Random.Range(0, InactiveEventBooths.Count)].SetActivity();
    }
    public void SpawnVisitor()
    {
        if (visitorPrefabs.Count == 0) { Debug.Log("No visitor prefabs set"); return; }
        if (ExitPoints.Count == 0) { return; }

        var visitor = Instantiate(visitorPrefabs[Random.Range(0, visitorPrefabs.Count)],
            ExitPoints[Random.Range(0, ExitPoints.Count)].transform.position, Quaternion.identity);
        SpawnedVisitors.Add(visitor);
        OnVisitorSpawned.Invoke(visitor);
    }
    public void SpawnStaff()
    {
        if (staffPrefabs.Count == 0) { Debug.Log("No staff prefabs set"); return; }
        if (InactiveEventBooths.Count == 0) { return; }

        SpawnedStaffs.Add(
        Instantiate(staffPrefabs[Random.Range(0, staffPrefabs.Count)], InactiveEventBooths[Random.Range(0, InactiveEventBooths.Count)].transform.position, Quaternion.identity
            ));
        OnSpawnedStaff.Invoke(SpawnedStaffs);
    }
}
