using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager instance;

    [SerializeField] private int negativePointsPerLife = 10;
    [SerializeField] private int StartLifes = 10;
    [SerializeField] private UnityEvent onNegativeScore;
    [SerializeField] private UnityEvent onLifeLost;
    [SerializeField] private UnityEvent onGameLost;


    private void Awake()
    {
        if (instance == null) { instance = this; }
        else if (instance != this) { Destroy(gameObject); }
    }

    public int CurrentLifes { get; set; }
    public int CurrentNegativePoints { get; set; }



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
            { onGameLost.Invoke(); }
        }
    }
}
