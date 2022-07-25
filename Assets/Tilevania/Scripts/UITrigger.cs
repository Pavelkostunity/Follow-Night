using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UITrigger : MonoBehaviour
{
    BoxCollider2D mycollider;
    [SerializeField] GameObject[] activate;
    [SerializeField] GameObject[] deactivate;
    private void Start()
    {
        mycollider = GetComponent<BoxCollider2D>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
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
}
