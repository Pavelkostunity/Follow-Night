using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameFlow : MonoBehaviour
{
    [SerializeField] GameObject Startpoint;
    [SerializeField] GameObject Bosspoint;
    Vector3 spawnpoint;
    [SerializeField] bool isbossfight = false;
    PlayAudio musicplayer;

    private void Awake()
    {
        SetUpSingleton();
        RemembertheTransform(isbossfight);
        musicplayer = GetComponent<PlayAudio>();
    }
    private void SetUpSingleton()
    {
        int numberGameSessions = FindObjectsOfType<GameFlow>().Length;
        if (numberGameSessions > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }
    // Start is called before the first frame update
    public void SetSpawnpoint(bool isboss)
    {
        isbossfight = isboss;
        RemembertheTransform(isboss);
    }
    public void RemembertheTransform(bool isboss)
    {
        if (isbossfight)
        {
            spawnpoint = Bosspoint.transform.position;
            Debug.Log("I've set to spawn at boss pit");
        }
        else
        {
            spawnpoint = Startpoint.transform.position;
        }
    }
    public void StartScene()
    {
        var currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
    }
    public Vector3 ReturnTransform()
    {
        return spawnpoint;
    }
    public void ExitGame()
    {
        Application.Quit();
    }    
}
