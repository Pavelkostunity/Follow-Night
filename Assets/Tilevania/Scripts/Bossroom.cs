using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bossroom : MonoBehaviour
{
    [SerializeField] bool isrolling = false;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Rolling();
    }
    private void Rolling()
    {
        if (isrolling)
        {
            transform.Rotate(0, 0, -0.1f);
        }
        else
        {
            bool isflat = gameObject.transform.rotation == new Quaternion(0, 0, 0.707106829f, 0.707106829f) ||
                gameObject.transform.rotation == new Quaternion(0, 0, -0.707106829f, 0.707106829f) ||
                gameObject.transform.rotation == new Quaternion(0, 0, 0, 1) ||
                gameObject.transform.rotation == new Quaternion(0, 0, 1, 0);
            
            if (!isflat)
            {
                transform.Rotate(0, 0, -0.1f);
            }
            else transform.Rotate(0, 0, 0);

        }
    }
    public void StartRolling()
    {
        isrolling = true;
    }
    public void StopRolling()
    {
        isrolling = false;
    }
}
