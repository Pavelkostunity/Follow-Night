using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    Rigidbody2D myRigidBody;
    Animator myAnimator;
    BoxCollider2D mycollider;
    [SerializeField] float health = 100;
    [SerializeField] GameObject attackpos;
    int attackcount = 0;
    [SerializeField] int numberofattacks = 6;
    [SerializeField] float timebetweenattacks = 10f;
    [SerializeField] Enemy carl;
    [SerializeField] Enemy max;
    [SerializeField] Enemy beam;
    [SerializeField] Enemy point;
    [SerializeField] GameObject[] enemyspawnpoints;
    [SerializeField] GameObject[] beamsspawnpoints;
    Player player;
    // [SerializeField] Beam beam;
    // [SerializeField] Point point;

    // Start is called before the first frame update
    void Start()
    {
        myRigidBody = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
        mycollider = GetComponent<BoxCollider2D>();
        StartCoroutine(Entering(4f));
        player = FindObjectOfType<Player>();
    }
    IEnumerator Entering(float time)
    {
        Vector3 startingPos = transform.position;
        Vector3 finalPos = attackpos.transform.position;
        float elapsedTime = 0;

        while (elapsedTime < time)
        {
            transform.position = Vector3.Lerp(startingPos, finalPos, (elapsedTime / time));
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        StartCoroutine(ChooseAttack());
    }
    IEnumerator ChooseAttack()
    {
        Debug.Log("I'm Choosing attack");
        if (attackcount == numberofattacks)
        {
            StartCoroutine(Transfer());
        }
        else
        {
            int i = Mathf.RoundToInt(Random.Range(0.51f, 4.49f));
            switch (i)
            {
                case 1:
                    StartCoroutine(Attack1());
                    break;
                case 2:
                    StartCoroutine(Attack2());
                    break;
                case 3:
                    StartCoroutine(Attack3());
                    break;
                case 4:
                    StartCoroutine(Attack4());
                    break;
            }
        }
        yield return null;
    }
    IEnumerator Transfer()
    {
        Debug.Log("Transfer");
        int damagedealt = 0;
        // убрать боссу неуязвимость, закрутить зону, отодвинуть камеру, добавить какие-то периодичные атаки
        while (damagedealt <20)
            {
                yield return null;
            }
        StartCoroutine(EndTransfer());

    }
    IEnumerator EndTransfer()
    {
        yield return null;
        //вернуть боссу неуязвимость и в целом зону на место
    }
    IEnumerator Attack1()
    {
        Debug.Log("Attack 1");
        SpawnEnemy(enemyspawnpoints, carl) ;
        SpawnEnemy(enemyspawnpoints, carl);
        yield return new WaitForSeconds(timebetweenattacks);
        attackcount++;
        StartCoroutine(ChooseAttack());
    }
    IEnumerator Attack2()
    {
        Debug.Log("Attack 2");
        SpawnEnemy(enemyspawnpoints, max);
        SpawnEnemy(enemyspawnpoints, max);
        //как сделать так чтобы они были повернуты всегда к player возможно в самом скрипте Max
        yield return new WaitForSeconds(timebetweenattacks);
        attackcount++;
        StartCoroutine(ChooseAttack());
    }
    IEnumerator Attack3()
    {
        Debug.Log("Attack 3");
        StartCoroutine(SpawnABeam());
        yield return new WaitForSeconds(timebetweenattacks);
        attackcount++;
        StartCoroutine(ChooseAttack());
    }
    IEnumerator SpawnABeam()
    {
        int j = 0;
        while (j < 5)
        {
            SpawnEnemy(beamsspawnpoints, beam);
            yield return new WaitForSeconds(1f);
            j++;
        }
    }
    IEnumerator Attack4()
    {
        Debug.Log("Attack 4");
        StartCoroutine(SpawnAPoint());
        yield return new WaitForSeconds(timebetweenattacks);
        attackcount++;
        StartCoroutine(ChooseAttack());
    }
    IEnumerator SpawnAPoint()
    {
        int j = 0;
        while (j < 5)
        {
            Vector3 playerpos = new Vector3 (player.transform.position.x,player.transform.position.y,player.transform.position.z);
            Instantiate(point,playerpos, Quaternion.identity);
            yield return new WaitForSeconds(0.5f);
            j++;
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
    private void SpawnEnemy(GameObject[] spawnpoints, Enemy enemy)
    {
        int j = Mathf.RoundToInt(Random.Range(0, spawnpoints.Length));   //можно написать метод нормальный ибо часто используется
        Instantiate(enemy, spawnpoints[j].transform);
    }
}
