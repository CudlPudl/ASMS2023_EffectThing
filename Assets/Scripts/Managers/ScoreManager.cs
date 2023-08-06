using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager instance;

    [SerializeField] private int negativePointsPerLife = 10;
    [SerializeField] private int StartLifes = 10;
    [SerializeField] private TMPro.TMP_Text scoreTxt;
    [SerializeField] private TMPro.TMP_Text timerTxt;
    [SerializeField] public UnityEvent onGameStart;
    [SerializeField] public UnityEvent onNegativeScore;
    [SerializeField] public UnityEvent onLifeLost;
    [SerializeField] public UnityEvent onGameLost;


    private void Awake()
    {
        if (instance == null) { instance = this; }
        else if (instance != this) { Destroy(gameObject); }
    }

    public int CurrentLifes { get; set; }
    public int CurrentNegativePoints { get; set; }

    public bool GameOn { get; private set; } = false;
    private float timer = 0.0f;
    public float Timer
    {
        get => timer;
        private set
        {
            timer = value;
            timerTxt.text = $"Time:    {Mathf.FloorToInt(Timer / 60f)} : {Mathf.FloorToInt(Timer % 60)}";
        }
    }

    public void OnStart()
    {
        GameOn = true;
        Timer = 0.0f;
        CurrentLifes = ObjectManager.instance.SpawnedStaffs.Count;
        CurrentNegativePoints = 0;
        onGameStart.Invoke();
    }

    private void Update()
    {
        if (GameOn) { Timer += Time.deltaTime; }
        else
        {
            if (ObjectManager.instance.SpawnedVisitors.Count > 0)
            {
                ObjectManager.instance.SpawnedVisitors[0].Despawn();
            }

            if (ObjectManager.instance.SpawnedStaffs.Count > 0)
            {
                ObjectManager.instance.SpawnedStaffs[0].Despawn();
            }
        }
    }


    public void ReducePoint()
    {
        CurrentNegativePoints++;
        onNegativeScore.Invoke();

        if (CurrentNegativePoints > negativePointsPerLife)
        {
            CurrentNegativePoints -= negativePointsPerLife;
            CurrentLifes--;
            onLifeLost.Invoke();

            if (CurrentLifes <= 0)
            {
                scoreTxt.text = $"You Survived {Mathf.FloorToInt(Timer / 60f)} minutes and {Mathf.FloorToInt(Timer % 60)} seconds!";
                GameOn = false;
                onGameLost.Invoke();
            }
        }
    }
}
