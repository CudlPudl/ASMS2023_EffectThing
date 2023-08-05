using System.Collections;
using System.Collections.Generic;
using RootieSmoothie;
using RootieSmoothie.Audio;
using UnityEngine;

public class RandomAudioCaller : MonoBehaviour
{
    public List<AudioClip> clips;

    public void Play()
    {
        if (clips.Count == 0)
            return;
        AudioManager.Instance.PlaySound(this, clips[(int)RandomHelper.GetRandomRange(0, clips.Count)]);
    }
}
