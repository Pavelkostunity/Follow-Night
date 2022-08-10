using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    [Header("Common Stuff")]
    Animator myAnimator;
    AudioSource myaudiosourse;
    [SerializeField] int health = 100;
    [SerializeField] GameObject attackpos;
    int attackcount = 0;
    [SerializeField] float enteringtime = 4f;
    [Header("Attack phaze")]
    [SerializeField] int numberofattacks = 6;
    [SerializeField] float timebetweenattacks = 10f;
    [SerializeField] float timebetweentranferattack = 5f;
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
    [Header ("Death")]
    [SerializeField] GameObject[] activate;
    [SerializeField] GameObject[] deactivate;
    [SerializeField] ParticleSystem deathvfx;


    // Start is called before the first frame update
    void Start()
    {
        myAnimator = GetComponent<Animator>();
        inv = true;
        myaudiosourse = GetComponent<AudioSource>();
        StartCoroutine(Entering(enteringtime));
        player = FindObjectOfType<Player>();
        
    }
    IEnumerator Entering(float time)
    {
        Startbossmusic();
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
        if (attackcount == numberofattacks)
        {
            RoomCamera();
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
                    StartCoroutine(Attack3(5,timebetweenattacks,false));
                    break;
                case 4:
                    StartCoroutine(Attack4(5,timebetweenattacks,false));
                    break;
            }
        }
        yield return null;
    }
    IEnumerator Attack1()
    {
        SpawnEnemy(enemyspawnpoints, carl) ;
        SpawnEnemy(enemyspawnpoints, carl);
        yield return new WaitForSeconds(timebetweenattacks);
        attackcount++;
        StartCoroutine(ChooseAttack());
    }
    IEnumerator Attack2()
    {
        SpawnEnemy(enemyspawnpoints, max);
        SpawnEnemy(enemyspawnpoints, max);
        yield return new WaitForSeconds(timebetweenattacks);
        attackcount++;
        StartCoroutine(ChooseAttack());
    }
    IEnumerator Attack3(int numberofbeams,float time, bool transfer)
    {
        StartCoroutine(SpawnABeam(numberofbeams));
        yield return new WaitForSeconds(time);
        attackcount++;
        if (transfer)
        {
            StartCoroutine(Transfer());
        }
        else
        {
            StartCoroutine(ChooseAttack());
        }
        
    }
    IEnumerator SpawnABeam(int numberofbeam)
    {
        int j = 0;
        while (j < numberofbeam)
        {
            SpawnEnemy(beamsspawnpoints, beam);
            yield return new WaitForSeconds(1f);
            j++;
        }
    }
    IEnumerator Attack4(int numberofpoints, float timeb, bool transfer)
    {
        StartCoroutine(SpawnAPoint(numberofpoints));
        yield return new WaitForSeconds(timeb);
        attackcount++;
        if (transfer)
        {
            StartCoroutine(Transfer());
        }
        else
        {
            StartCoroutine(ChooseAttack());
        }
    }
    IEnumerator SpawnAPoint(int numberofpoint)
    {
        int j = 0;
        while (j < numberofpoint)
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
        if (damageduringtranfer > 20)
        {
            StartCoroutine(EndTransfer());
        }
        else
        {
            int i = Mathf.RoundToInt(Random.Range(0.51f, 2.49f));
            switch (i)
            {
                case 1:
                    StartCoroutine(Attack3(2, timebetweentranferattack, true));
                    break;
                case 2:
                    StartCoroutine(Attack4(3, timebetweentranferattack, true));
                    break;
            }
        }
        yield return null;
    }

    private void RoomCamera()
    {
        damageduringtranfer = 0;
        bossroom.StartRolling(); // start of room rolling
        bossfightcontroller.StartTransfer(); //camera movement
        myAnimator.SetBool("Idle", true);
        bossreachpoints.SetActive(true);
        inv = false;
    }

    IEnumerator EndTransfer()
    {
        bossroom.StopRolling();
        bossfightcontroller.EndTranfer();
        myAnimator.SetBool("Idle", false);
        bossreachpoints.SetActive(false);
        inv = true;
        yield return new WaitForSeconds(3f);
        attackcount = 0;
        StartCoroutine(ChooseAttack());
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
                StopAllCoroutines();
                StartCoroutine(Die());
            }
        }
    }
    IEnumerator Die()
    {
        Time.timeScale = 0.2f;
        deathvfx.Play();
        yield return new WaitForSecondsRealtime(5f);
        Time.timeScale = 0f;
        foreach (GameObject act in activate)
        {
            act.SetActive(true);
        }
        foreach (GameObject deact in deactivate)
        {
            deact.SetActive(false);
        }
        Destroy(gameObject);
    }
    public void Startbossmusic()
    {
        myaudiosourse.Play();
    }
    public int GetHealth()
    {
        return health;
    }
}
