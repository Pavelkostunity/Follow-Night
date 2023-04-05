using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossHP : MonoBehaviour
{
    [SerializeField] Boss boss;
    Slider lives;
    public Gradient gradient;
    public Image fill;
    // Start is called before the first frame update
    void Start()
    {
       lives = GetComponent<Slider>();
       lives.enabled = true;
       lives.maxValue = boss.GetHealth();

       fill.color = gradient.Evaluate(1f);
    }
    void Update()
    {
        lives.value = boss.GetHealth();
        fill.color = gradient.Evaluate(lives.normalizedValue);
    }
}
