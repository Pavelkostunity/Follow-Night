using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossFightStart : MonoBehaviour
{
    Animator myanimator;
    BoxCollider2D mycollider;
    [SerializeField] GameObject[] activate;
    [SerializeField] GameObject[] deactivate;
    private void Start()
    {
        myanimator = GetComponent<Animator>();
        mycollider = GetComponent<BoxCollider2D>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        myanimator.SetBool("BossFight", true);
        foreach (GameObject act in activate)
        {
            act.SetActive(true);
        }
        foreach (GameObject deact in deactivate)
        {
            deact.SetActive(false);
        }
        mycollider.enabled = false;
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
