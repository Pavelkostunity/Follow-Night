using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Healthbar : MonoBehaviour
{
    Player player;
    Text lives;
    // Start is called before the first frame update
    void Start()
    {
        lives = GetComponent<Text>();
        lives.enabled = true;
        player = FindObjectOfType<Player>();
    }
    void Update()
    {
        lives.text = player.GetHealth().ToString() + " HP";
    }
}
