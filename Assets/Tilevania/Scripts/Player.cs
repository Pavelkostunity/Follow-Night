using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    Rigidbody2D myRigidBody;
    Animator myAnimator;
    [SerializeField] float runSpeed = 5f;
    [SerializeField] float jumpSpeed = 5f;
    bool isAlive = true;
    BoxCollider2D myFeet;
    PolygonCollider2D myhitbox;
    [SerializeField] int health = 200;
    [SerializeField] Vector2 deathkick = new Vector2(25f,25f);
    [Header("Sword Settings")]
    [SerializeField] GameObject sword;
    [SerializeField] GameObject horz;
    [SerializeField] GameObject up;
    [SerializeField] GameObject bot;
    // Start is called before the first frame update
    void Start()
    {
        myRigidBody = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
        myFeet = GetComponent<BoxCollider2D>();
        myhitbox = GetComponent<PolygonCollider2D>();
    }
    void Update()
    {
        if (!isAlive) { return; };
        Run();
        FlipSprite();
        Jump();
        Hit();
    }
    private void Run()
    {
        float controlThrow = Input.GetAxis("Horizontal");
        Vector2 playerVelocity = new Vector2(controlThrow*runSpeed, myRigidBody.velocity.y);
        myRigidBody.velocity = playerVelocity;
        bool playerHasHorizontalSpeed = Mathf.Abs(myRigidBody.velocity.x) > Mathf.Epsilon;
        if (playerHasHorizontalSpeed)
        {
            myAnimator.SetBool("Running", true);
        }
        else { myAnimator.SetBool("Running", false); }
    }
    private void Jump()
    {
        if (!myFeet.IsTouchingLayers(LayerMask.GetMask("Foreground")))
        {return;}
        if (Input.GetButtonDown("Jump"))
        {
            Vector2 jumpVelocityToAdd = new Vector2(0f, jumpSpeed);
            myRigidBody.velocity += jumpVelocityToAdd;
        }
    }
    private void FlipSprite()
    {
        bool playerHasHorizontalSpeed = Mathf.Abs(myRigidBody.velocity.x) > Mathf.Epsilon;
        if (playerHasHorizontalSpeed)
        {
            transform.localScale = new Vector2(Mathf.Sign(myRigidBody.velocity.x), 1f);
        }
    }
    private void Hit()
    {
        GameObject direct;
        if (Input.GetButtonDown("Fire1"))
        {
            if ((!myFeet.IsTouchingLayers(LayerMask.GetMask("Ground"))) && (Input.GetKey("down")))
            {
                direct = bot;
            }
            else if (Input.GetKey("up"))
            {
                direct = up;
            }
            else direct = horz;
            Instantiate(sword, direct.transform.position, direct.transform.rotation, direct.transform);
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (myhitbox.IsTouchingLayers(LayerMask.GetMask("Enemy")) || myFeet.IsTouchingLayers(LayerMask.GetMask("Enemy")))
        {
            DamageDealer damageDealer = other.gameObject.GetComponent<DamageDealer>();
            bool invisibl = myAnimator.GetBool("GotHit");
            if (!damageDealer)
            {
                return;
            }
            if (invisibl)
            {
                return;
            }
            ProcessHit(damageDealer);
        }
    }
    private void ProcessHit(DamageDealer damageDealer)
    {
        health -= damageDealer.GetDamage();
        myAnimator.SetBool("GotHit", true);
        if (health <= 0)
        {
            Die();
        }
    }
    public void Die()
    {
        StartCoroutine(Death());
    }
    IEnumerator Death()
    {
        Time.timeScale = 0.2f;
        FindObjectOfType<YouDead>().ShowDeathText();
        yield return new WaitForSecondsRealtime(2f);
        Time.timeScale = 1f;
        var currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
    }
    public void Pushback(Vector2 pushway)
    {
        myRigidBody.velocity = pushway;
    }
    public void StopHitAn()
    {
        myAnimator.SetBool("GotHit", false);
    }
    public int GetHealth()
    {
        return health;
    }
}
