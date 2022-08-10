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

    private void Awake()
    {
        SetUpSingleton();
        RemembertheTransform();
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
    public void SetSpawnpoint(bool isboss)
    {
        isbossfight = isboss;
        RemembertheTransform();
    }
    public void RemembertheTransform()
    {
        if (isbossfight)
        {
            spawnpoint = Bosspoint.transform.position;
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
        Time.timeScale = 1f;
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
