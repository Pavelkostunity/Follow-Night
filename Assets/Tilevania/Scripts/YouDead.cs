using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class YouDead : MonoBehaviour
{
    public void ShowDeathText()
    {
        Text deathtext = GetComponent<Text>();
        deathtext.enabled = true;
    }
}
