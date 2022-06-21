using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    Rigidbody2D myRigidBody;
    Animator myAnimator;
    [SerializeField] float runSpeed = 5f;
    [SerializeField] float jumpSpeed = 5f;
    bool isAlive = true;
    CapsuleCollider2D myBodyCollider;
    BoxCollider2D myFeet;
    PolygonCollider2D myhitbox;
    [SerializeField] int health = 200;
    float gravityScaleAtStart;
    [SerializeField] Vector2 deathkick = new Vector2(25f,25f);
    [Header("Sword Settings")]
    [SerializeField] GameObject sword;
    [SerializeField] GameObject horz;
    [SerializeField] GameObject up;
    [SerializeField] GameObject bot;
    Vector2 pushway;
    // Start is called before the first frame update
    void Start()
    {
        myRigidBody = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
        myBodyCollider = GetComponent<CapsuleCollider2D>();
        myFeet = GetComponent<BoxCollider2D>();
        myhitbox = GetComponent<PolygonCollider2D>();
        gravityScaleAtStart = myRigidBody.gravityScale;
    }
    void Update()
    {
        if (!isAlive) { return; };
        Run();
        FlipSprite();
        Jump();
        Die();
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
                pushway = new Vector2(100, 100);
            }
            else if (Input.GetKey("up"))
            {
                direct = up;
                pushway = new Vector2(0, 0);
            }
            else direct = horz;
            pushway = new Vector2(0, 20);
            Instantiate(sword, direct.transform.position, direct.transform.rotation, direct.transform);
        }
    }
    private void Die()
    {
        if (myBodyCollider.IsTouchingLayers(LayerMask.GetMask("Enemy", "Hazards")))
        {
            isAlive = false;
            myAnimator.SetTrigger("Die");
            GetComponent<Rigidbody2D>().velocity = deathkick;
            FindObjectOfType<GameSession>().ProcessPlayerDeath();
        }
    }
    public void Pushback(Vector2 pushway)
    {
        myRigidBody.velocity = pushway;
    }
}
