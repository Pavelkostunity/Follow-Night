using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    [Header("Common Stuff")]
    Rigidbody2D myRigidBody;
    Animator myAnimator;
    BoxCollider2D mycollider;
    [SerializeField] int health = 100;
    [SerializeField] GameObject attackpos;
    int attackcount = 0;
    [Header("Attack phaze")]
    [SerializeField] int numberofattacks = 6;
    [SerializeField] float timebetweenattacks = 10f;
    [SerializeField] Enemy carl;
    [SerializeField] Enemy max;
    [SerializeField] Enemy beam;
    [SerializeField] Enemy point;
    [SerializeField] GameObject[] enemyspawnpoints;
    [SerializeField] GameObject[] beamsspawnpoints;
    Player player;
    bool inv = false;
    [Header ("Tranfer phaze")]
    int damageduringtranfer = 0;  
    [SerializeField] Bossroom bossroom;
    [SerializeField] BossFightStart bossfightcontroller;
    [SerializeField] GameObject bossreachpoints;

    // Start is called before the first frame update
    void Start()
    {
        myRigidBody = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
        mycollider = GetComponent<BoxCollider2D>();
        inv = true;
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
    private void SpawnEnemy(GameObject[] spawnpoints, Enemy enemy)
    {
        int j = Mathf.RoundToInt(Random.Range(0, spawnpoints.Length));   //можно написать метод нормальный ибо часто используется
        Instantiate(enemy, spawnpoints[j].transform);
    }
    IEnumerator Transfer()
    {
        Debug.Log("Transfer");
        damageduringtranfer = 0;
        bossroom.StartRolling(); // start of room rolling
        bossfightcontroller.StartTransfer(); //camera movement
        myAnimator.SetBool("Idle", true);
        bossreachpoints.SetActive(true);
        inv = false;
        // убрать боссу неуязвимость, закрутить зону, отодвинуть камеру, добавить какие-то периодичные атаки
        while (damageduringtranfer < 20)
        {
            yield return null;
        }
        StartCoroutine(EndTransfer());

    }
    IEnumerator EndTransfer()
    {
        Debug.Log("EndTransfer");
        bossroom.StopRolling();
        bossfightcontroller.EndTranfer();
        myAnimator.SetBool("Idle", false);
        bossreachpoints.SetActive(false);
        inv = true;
        yield return new WaitForSeconds(3f);
        attackcount = 0;
        StartCoroutine(ChooseAttack());
    }
    // Update is called once per frame
    void Update()
    {
        
    }
      private void OnTriggerEnter2D(Collider2D other)
    {
        DamageDealer damageDealer = other.gameObject.GetComponent<DamageDealer>();
        if (!damageDealer)
        {
            return;
        }
        ProcessHit(damageDealer);
    }
    private void ProcessHit(DamageDealer damageDealer)
    {
        if (inv == false)
        {
            health -= damageDealer.GetDamage();
            damageduringtranfer++;
            if (health <= 0)
            {
                Die();
            }
        }
    }
    private void Die()
    {

    }
    public int GetHealth()
    {
        return health;
    }
}
