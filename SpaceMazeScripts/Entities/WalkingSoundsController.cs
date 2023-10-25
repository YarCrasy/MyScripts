using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkingSoundsController : MonoBehaviour
{
    [SerializeField] AudioSource source;
    [SerializeField] AudioClip[] sounds;

    AudioClip lastSound;

    public void PlayAudio(int index)
    {
        if (index < sounds.Length && index >= 0)
        {
            source.PlayOneShot(sounds[index]);
        }
        lastSound = sounds[index];
    }

    public void PlayRndAudio()
    {
        int rnd;
        do
        {
            rnd = Random.Range(0, sounds.Length);
        } while (sounds[rnd] == lastSound);
        
        PlayAudio(rnd);
    }

}
