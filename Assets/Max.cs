using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Max : MonoBehaviour
{
    [SerializeField] GameObject laserPrefab;
    [SerializeField] float projectileSpeed = 2f;
    [SerializeField] float projectileFiringPeriod = 2f;
    bool isfacingright = true;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Attack());
    }

    // Update is called once per frame
    void Update()
    {
        FindPlayer();
    }

    private void FindPlayer()
    {
        Player player = FindObjectOfType<Player>();
        if (transform.position.x > player.transform.position.x)
        {
            if (isfacingright)
            {
                isfacingright = false;
                transform.Rotate(0, 180, 0, Space.Self);
                projectileSpeed = -projectileSpeed;
            }
        }
        else
        {
            if (!isfacingright)
            {
                isfacingright = true;
                transform.Rotate(0, 180, 0, Space.Self);
                projectileSpeed = -projectileSpeed;
            }
        }
        //how to turn to player
    }


    IEnumerator Attack()
    {
        while (true)
        {
            yield return new WaitForSeconds(projectileFiringPeriod);
            GameObject laser = Instantiate(laserPrefab,
            transform.position,
            Quaternion.identity) as GameObject;
            laser.GetComponent<Rigidbody2D>().velocity = new Vector2(projectileSpeed, 0);
            
        }
    }    
}
