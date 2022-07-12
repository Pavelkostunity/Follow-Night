using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameFlow : MonoBehaviour
{
    AudioSource myaudiosourse;
    // Start is called before the first frame update
    void Start()
    {
        myaudiosourse = GetComponent<AudioSource>();
    }
    public void Startbossmusic()
    {
        if (myaudiosourse.isPlaying == true)
        {
            return;
        }
        myaudiosourse.Play();
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
