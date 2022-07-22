using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameFlow : MonoBehaviour
{
    Vector3 spawnpoint;
    AudioSource myaudiosourse;
    private void Awake()
    {
        spawnpoint = transform.position;
        DontDestroyOnLoad(this.gameObject);
        myaudiosourse = GetComponent<AudioSource>();
    }
    // Start is called before the first frame update
    void Start()
    {
        Startmusic();
    }

    private void Startmusic()
    {
         myaudiosourse.Play();
    }

    public void RemembertheTransform(Vector3 checkpoint)
    {
        spawnpoint = checkpoint;
    }
    public Vector3 ReturnTransform()
    {
        return spawnpoint;
    }
    public void Stopmusic()
    {
        myaudiosourse.Stop();
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
