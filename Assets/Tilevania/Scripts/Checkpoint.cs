using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    GameFlow gameflow;
    // Start is called before the first frame update
    void Start()
    {
        gameflow = FindObjectOfType<GameFlow>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        gameflow.SetSpawnpoint(true);
    }
}
