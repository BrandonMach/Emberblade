using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolAI : MonoBehaviour
{
    // Denna Script används för enemies som bee som patrollar runt till spelaren är i range för att attackera
    public float speed = 10f;
    public float range = 50f;
    public Animator animator;
    float startingX;
    int dir = 1;
    bool idle;
    bool fliped;
    Rigidbody2D rb2d;

    //Time to attack
    bool chargeTime;
    float currentTime = 0f;
    float startingTime = 0.7f;

    //Attack
    bool getAattackPos;
    bool attackTime;
    public float attackAcc = 2;
    public float attackSpeed = 1;
    Vector2 attackDir;

    // Target
    private Vector2 movetowardsPlayer;
    private PlayerInfo playerInfoController;
    public float agroRangeX = 70;
    public float agroRangeY = 30;

    int invokeCounter = 0;
    float timer;

    void Start()
    {
        currentTime = startingTime;
        idle = true;
        startingX = transform.position.x;
        playerInfoController = GameObject.Find("Player").GetComponent<PlayerInfo>();
        rb2d = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {

        Idle();
        Charge();
        Attack();
    }
    public void Charge()
    {
        if (chargeTime)
        {
            animator.SetBool("IsCharing", true);
            currentTime -= 1 * Time.deltaTime;
            Debug.Log(currentTime);
            if (currentTime <= 0)
            {
                chargeTime = false;
                attackTime = true;
                getAattackPos = true;
            }
        }

    }

    public void Attack()
    {
        if (attackTime)
        {
            animator.SetBool("IsCharing", false);
            animator.SetBool("IsAttacking", true);


            float angle = Mathf.Atan2(attackDir.x, attackDir.y) * Mathf.Rad2Deg;
            attackDir.Normalize();
            movetowardsPlayer = attackDir;


            Invoke("getDelayedPos", 0.1f);
            rb2d.AddForce(attackDir * attackAcc, ForceMode2D.Impulse);


            if (transform.position.x < playerInfoController.transform.position.x)
            {
                transform.localScale = new Vector2(-1, 1);
                fliped = false;
            }
            else
            {
                transform.localScale = new Vector2(1, 1);
                fliped = true;
            }
        }
    }


    private void getDelayedPos()
    {
        if (invokeCounter <= 1)
        {
            attackDir = playerInfoController.transform.position - transform.position;
        }

        invokeCounter++;

        if (invokeCounter > 1)
        {
            timer += Time.deltaTime;
            if (timer > 0.05f)
            {
                attackDir = playerInfoController.transform.position - transform.position;
                attackAcc += 1f;
                timer = 0;
                Debug.Log(timer);
            }
        }
    }


    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player") && attackTime)
        {
            playerInfoController.TakeDamage(40);
            Destroy(gameObject);
        }
        if (other.gameObject.CompareTag("Wall") && attackTime)
        {
            Destroy(gameObject);
        }
        if (other.gameObject.CompareTag("Ground") && attackTime)
        {
            Destroy(gameObject);
        }
        if (other.gameObject.CompareTag("Roof") && attackTime)
        {
            Destroy(gameObject);
        }
    }

    public void Idle()
    {
        if (idle)
        {
            transform.Translate(Vector2.left * speed * Time.deltaTime * dir);
            if (transform.position.x < startingX || transform.position.x > startingX + range)
            {
                dir *= -1;
                Flip();
            }

            Collider2D[] hitColliders = Physics2D.OverlapBoxAll(transform.position, new Vector2(agroRangeX, agroRangeY), 0);

            foreach (var colliderHit in hitColliders)
            {
                if (colliderHit.gameObject.CompareTag("Player"))
                {
                    idle = false;
                    chargeTime = true;
                }
            }
        }
        
    }

    public void Flip()
    {
        transform.localScale = new Vector2(transform.localScale.x * -1, transform.localScale.y);

        if (transform.localScale.x > 0)
        {
            fliped = true;
        }
        else if (transform.localScale.x < 0)
        {
            fliped = false;
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position, new Vector2(agroRangeX, agroRangeY));
    }
}
