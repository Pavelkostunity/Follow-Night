using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossHP : MonoBehaviour
{
    [SerializeField] Boss boss;
    Text lives;
    // Start is called before the first frame update
    void Start()
    {
        lives = GetComponent<Text>();
        lives.enabled = true;
    }
    void Update()
    {
        lives.text = boss.GetHealth().ToString();
    }
}
