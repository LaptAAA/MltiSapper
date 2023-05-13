using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicController : MonoBehaviour
{
    public AudioSource audioSource;

    public void WorkWithMusic()
    {
        audioSource.mute = !audioSource.mute;
    }

    public void IncreaseVolume()
    {
        if (audioSource.volume < 1)
        {
            audioSource.volume += 0.1f;
        }
        
    }
    public void ReduceVolume()
    {
        if (audioSource.volume > 0)
        {
            audioSource.volume -= 0.1f;
        }
    }
}
