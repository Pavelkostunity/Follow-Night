using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossFightStart : MonoBehaviour
{
    Animator myanimator;
    [SerializeField] GameObject[] activate;
    [SerializeField] GameObject[] deactivate;
    GameFlow gameflow;
    private void Start()
    {
        myanimator = GetComponent<Animator>();
        gameflow = FindObjectOfType<GameFlow>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        myanimator.SetBool("BossFight", true);
        gameflow.Startbossmusic();
        foreach (GameObject act in activate)
        {
            act.SetActive(true);
        }
        foreach (GameObject deact in deactivate)
        {
            deact.SetActive(false);
        }
    }
    public void StartTransfer()
    {
        myanimator.SetBool("Bosstransfer", true);
    }
    public void EndTranfer()
    {
        myanimator.SetBool("Bosstransfer", false);
    }
}
