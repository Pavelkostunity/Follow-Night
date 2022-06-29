using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] float health = 100;
    [SerializeField] GameObject[] activate;
    [SerializeField] GameObject[] deactivate;
    private void OnTriggerEnter2D(Collider2D other)
    {
        DamageDealer damageDealer = other.gameObject.GetComponent<DamageDealer>();
        if (!damageDealer)
        {
            return;
        }
        ProcessHit(damageDealer);
    }

    private void ProcessHit(DamageDealer damageDealer)
    {
        health -= damageDealer.GetDamage();
        if (health <= 0)
        {
            Die();
        }
    }
    private void Die()
    {     
       foreach (GameObject act in activate)
       {
            act.SetActive(true);      
       }
       foreach (GameObject deact in deactivate)
       {
            deact.SetActive(false);
       }
       Destroy(gameObject);
    }
}
