using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Beam : MonoBehaviour
{
    [SerializeField] float delaybeforeexplos = 8f;
    [SerializeField] float delaybeforedestroy = 2f;
    Enemy enemy;
    Animator myanimator;
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
        enemy.TurnOnInvin();
        yield return new WaitForSeconds(delaybeforeexplos);
        StartCoroutine(Hit());

    }
    IEnumerator Hit()
    {
        myanimator.SetBool("CA", true);
        transform.position = new Vector2(transform.position.x + 0.000001f, transform.position.y);
        yield return new WaitForSeconds(delaybeforedestroy);
        Destroy(gameObject);
    }
}
