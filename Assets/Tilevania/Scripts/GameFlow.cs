using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameFlow : MonoBehaviour
{
    Vector3 spawnpoint;
    private void Awake()
    {
        spawnpoint = transform.position;
        DontDestroyOnLoad(this.gameObject);
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }
    public void RemembertheTransform(Vector3 checkpoint)
    {
        spawnpoint = checkpoint;
    }
    public Vector3 ReturnTransform()
    {
        return spawnpoint;
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
