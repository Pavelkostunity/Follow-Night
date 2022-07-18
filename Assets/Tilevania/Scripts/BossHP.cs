using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossHP : MonoBehaviour
{
    [SerializeField] Boss boss;
    Slider lives;
    // Start is called before the first frame update
    void Start()
    {
       lives = GetComponent<Slider>();
       lives.enabled = true;
       lives.maxValue = boss.GetHealth();
    }
    void Update()
    {
        lives.value = boss.GetHealth();
    }
}
