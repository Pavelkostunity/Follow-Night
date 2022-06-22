using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class HKUI : MonoBehaviour
{
    VideoPlayer videopl;
    void Start()
    {
        StartCoroutine(StopVideo());
    }

    IEnumerator StopVideo()
    {
        yield return new WaitForSeconds(4.2f);
        videopl = GetComponent<VideoPlayer>();
        videopl.enabled = false;
    }
}
