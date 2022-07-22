using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Carl : MonoBehaviour
{
    [SerializeField] float attackperiod = 8f;
    Enemy enemy;
    Animator myanimator;
    CapsuleCollider2D bigcollider;
    public AudioClip morbintime;
    [SerializeField] bool isbossfight;
    ////в этом скрипте реализована атака
    // Start is called before the first frame update
    void Start()
    {
        myanimator = GetComponent<Animator>();
        enemy = GetComponent<Enemy>();
        StartCoroutine(Attack());
    }

    // Update is called once per frame
    void Update()
    {

    }
    IEnumerator Attack()
    {
        while (true)
        {
            StartCoroutine(Hit());
            yield return new WaitForSeconds(attackperiod);
        }
    }
    IEnumerator Hit()
    {
        enemy.TurnOnInvin();
        if (!isbossfight)
        {
            AudioSource.PlayClipAtPoint(morbintime, transform.position, 0.5f);
        }
        myanimator.SetBool("CA", true);
        transform.position = new Vector2(transform.position.x + 0.000001f, transform.position.y);
        yield return new WaitForSeconds(1f);
        myanimator.SetBool("CA", false);
        transform.position = new Vector2(transform.position.x - 0.000001f, transform.position.y);
        enemy.TurnOffInvin();
    }

}
