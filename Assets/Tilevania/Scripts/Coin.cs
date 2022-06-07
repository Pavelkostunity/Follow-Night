using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    [SerializeField] AudioClip coinPickUp;
    [SerializeField] float coinPickUpVol = 0.2f;
    [SerializeField] int coinsCost = 50;

     private void OnTriggerEnter2D(Collider2D collision)
    {
        AudioSource.PlayClipAtPoint(coinPickUp, Camera.main.transform.position, coinPickUpVol);
        FindObjectOfType<GameSession>().AddToScore(coinsCost);
        Destroy(gameObject);
    }
}
