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
    Transform player;
    [SerializeField] GameObject gun;
    // Start is called before the first frame update
    void Start()
    {
        myanimator = GetComponent<Animator>();
        StartCoroutine(Attack());
        player = FindObjectOfType<Player>().transform;
    }

    // Update is called once per frame
    void Update()
    {
        FindPlayer();
    }

    private void FindPlayer()
    {
        if (transform.position.x > player.position.x)
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
    }


    IEnumerator Attack()
    {
        while (true)
        {
            
            myanimator.SetTrigger("Idle");
            yield return new WaitForSeconds(projectileFiringPeriod);
            myanimator.SetTrigger("Attack");
            GameObject laser = Instantiate(laserPrefab,
            new Vector3 (gun.transform.position.x,gun.transform.position.y,gun.transform.position.z),
            Quaternion.identity) as GameObject;
            laser.GetComponent<Rigidbody2D>().velocity = new Vector2(projectileSpeed, 0);
            if (!isbossfight)
            {
                AudioSource.PlayClipAtPoint(piu, transform.position, 0.5f);
            }
        }
    }
    
}
