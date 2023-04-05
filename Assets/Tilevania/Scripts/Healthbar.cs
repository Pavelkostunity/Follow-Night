using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Healthbar : MonoBehaviour
{
    Player player;
    Slider lives;
    public Gradient gradient;
    public Image fill;
    // Start is called before the first frame update
    void Start()
    {
        lives = GetComponent<Slider>();
        lives.enabled = true;
        player = FindObjectOfType<Player>();
        lives.maxValue = player.GetHealth();
        fill.color = gradient.Evaluate(1f);
    }
    void Update()
    {
        lives.value = player.GetHealth();
        fill.color = gradient.Evaluate(lives.normalizedValue);
    }
}
