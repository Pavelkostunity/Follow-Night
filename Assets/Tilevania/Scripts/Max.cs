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
    Animator myanimator;
    [SerializeField] float deltax = 0f;
    [SerializeField] float deltay = 0f;
    public AudioClip piu;
    [SerializeField] bool isbossfight;
    // Start is called before the first frame update
    void Start()
    {
        myanimator = GetComponent<Animator>();
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
            if (!isbossfight)
            {
                AudioSource.PlayClipAtPoint(piu, transform.position, 0.5f);
            }
            myanimator.SetTrigger("Idle");
            yield return new WaitForSeconds(projectileFiringPeriod);
            myanimator.SetTrigger("Attack");
            GameObject laser = Instantiate(laserPrefab,
            new Vector3 (transform.position.x+deltax,transform.position.y+deltay,transform.position.z),
            Quaternion.identity) as GameObject;
            laser.GetComponent<Rigidbody2D>().velocity = new Vector2(projectileSpeed, 0);
        }
    }    
}
