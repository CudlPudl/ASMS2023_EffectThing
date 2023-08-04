using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSetupper : MonoBehaviour
{
    [SerializeField] byte activeBoothAtStart = 3;
    void Start()
    {
        for (int i = activeBoothAtStart; i > 0; i--) { ObjectManager.instance.SpawnBooth(); }

    }
}
