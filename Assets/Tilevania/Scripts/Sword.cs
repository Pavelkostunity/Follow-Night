using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : MonoBehaviour
{
    Vector2 pushway = new Vector2(0, 0);
    // Start is called before the first frame update
    Player Player;
    void Start()
    {
        Player = FindObjectOfType<Player>();
        if (transform.position.y < Player.transform.position.y)
        {
            pushway = new Vector2(0, 20);
        }
        StartCoroutine(Vanish());
    }
    IEnumerator Vanish()
    {
        yield return new WaitForSeconds(0.1f);
        Destroy(gameObject);
    }

    // Update is called once per frame
    private void OnTriggerEnter2D(Collider2D other)
    {
        //how to find what the pushway
        FindObjectOfType<Player>().Pushback(pushway);
    }
}
