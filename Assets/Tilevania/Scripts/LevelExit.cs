using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelExit : MonoBehaviour
{
    [SerializeField] float LevelLoadDelay = 2f;
    [SerializeField] float Slowmo = 0.2f;
    void OnTriggerEnter2D(Collider2D other)
    {
        StartCoroutine(LevelComplete());
    }
    IEnumerator LevelComplete()
    {
        Time.timeScale = Slowmo;
        yield return new WaitForSecondsRealtime(LevelLoadDelay);
        Time.timeScale = 1f;
        var currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex + 1);
    }
}
