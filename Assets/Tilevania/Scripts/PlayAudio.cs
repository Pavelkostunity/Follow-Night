using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayAudio : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip[] audioClipArray;
    bool isboss;
    void Start()
    {
        isboss = false;
        PlayMusic();
    }

    public void PlayMusic()
    {
        AudioClip newClip = audioClipArray[Random.Range(0, audioClipArray.Length)];
        audioSource.PlayOneShot(newClip);
    }
    private void Update()
    {
        if (!audioSource.isPlaying)
        {
            if (!isboss)
            {
                PlayMusic();
            }

        }
    }
    public void Stopmusic()
    {
        audioSource.Stop();
        isboss = true;
    }
}
