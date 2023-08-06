using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSetupper : MonoBehaviour
{
    [SerializeField] private int visitorsAtStart = 10;
    [SerializeField] private int staffsAtStart = 10;
    [SerializeField] private int activeBoothAtStart = 3;

    public void StartGame()
    {
        for (int i = activeBoothAtStart; i > 0; i--) { ObjectManager.instance.SpawnBooth(); }
        for (int i = visitorsAtStart; i > 0; i--) { ObjectManager.instance.SpawnVisitor(); }
        for (int i = staffsAtStart; i > 0; i--) { ObjectManager.instance.SpawnStaff(); }

        ScoreManager.instance.OnStart();
    }
}
