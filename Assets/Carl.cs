using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Carl : MonoBehaviour
{
    [SerializeField] float attackperiod = 8f;
    Enemy enemy;
    Animator myanimator;
    CapsuleCollider2D bigcollider;
    ////� ���� ������� ����������� �����
    // Start is called before the first frame update
    void Start()
    {
        myanimator = GetComponent<Animator>();
        enemy = GetComponent<Enemy>();
        bigcollider = GetComponent<CapsuleCollider2D>();
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
        myanimator.SetBool("CA", true);
        bigcollider.enabled = true;
        transform.position = new Vector2(transform.position.x + 0.000001f, transform.position.y);
        yield return new WaitForSeconds(1f);
        myanimator.SetBool("CA", false);
        bigcollider.enabled = false;
        transform.position = new Vector2(transform.position.x - 0.000001f, transform.position.y);
        enemy.TurnOffInvin();
    }

}
