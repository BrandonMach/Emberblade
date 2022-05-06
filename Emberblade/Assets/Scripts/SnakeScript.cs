using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeScript : MonoBehaviour
{
    // Start is called before the first frame update
    public bool playerInRange;
    public Vector2 playerDetection;
    PlayerInfo playerInfoController;
    public bool isOnGround;
    public bool facingLeft;
    public Animator animator;


    bool attacking;
    bool playAttackAnim;
    public Rigidbody2D rb;
    float startTimeAttackTimer;
    float attackDelay = 1;

    private float flipHitbox = 1f;
    float startTimer = 0;
    float animationTime = 0.95f;


    public Transform attackpoint;
    public float attackRange = 0.5f;
    public LayerMask playerLayer;
    Collider2D[] hitPlayer;




    void Start()
    {
        playerInfoController = GameObject.Find("Player").GetComponent<PlayerInfo>();
       // rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        DetectPlayer();
        if (attacking)
        {
            
            playAttackAnim = true;
            animator.SetBool("Attack", true);
           
           
            Invoke("StopAttackAnim", 3);
           
        }
        if (Input.GetKeyDown(KeyCode.Alpha7))
        {
            AttackPlayer();
           
        }


    }

    void DetectPlayer()
    {
        Vector3 characterScale = transform.localScale;
        playerInRange = false;

        Collider2D[] hitColliders = Physics2D.OverlapBoxAll(transform.position, playerDetection, 0);

        foreach (var colliderHit in hitColliders)
        {
            if (colliderHit.gameObject.CompareTag("Player"))
            {
                playerInRange = true;

                if (playerInRange)
                {
                   
                    if (this.transform.position.x > playerInfoController.transform.position.x && !facingLeft) // Armadillo på höger sida av spelare
                    {
                        characterScale.x *= -1;
                        facingLeft = true;
                        this.transform.localScale = characterScale;
                        Debug.Log("Armadillo look left");
                        flipHitbox *= -1;

                    }
                    if (this.transform.position.x < playerInfoController.transform.position.x && facingLeft) // Armadillo på vänster sida av spelaren
                    {
                        characterScale.x *= -1;
                        facingLeft = false;
                        this.transform.localScale = characterScale;
                        flipHitbox *= -1;

                     
                    }
                    StartAttack();
                }
            }
        }
    }

    void AttackPlayer()
    {
        animator.SetTrigger("ATrigger");


        hitPlayer = Physics2D.OverlapCircleAll(attackpoint.position, attackRange, playerLayer);
        foreach (Collider2D player in hitPlayer)
        {
            playerInfoController.TakeDamage(10);
        }


       
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        //if (other.gameObject.CompareTag("Player"))
        //{
        //    AttackPlayer();
        //}
    }

    void StartAttack()
    {
       
            if (facingLeft)
            {

                rb.AddForce(new Vector2(-8, 0), ForceMode2D.Impulse);
                 //AttackPlayer();
            }
            else
            {
                rb.AddForce(new Vector2(8, 0), ForceMode2D.Impulse);
                //AttackPlayer();
            }
        
    }
    void StopAttackAnim()
    {
        animator.SetBool("Attack", false);
       
    }

    private void OnDrawGizmosSelected()
    {
        
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position, playerDetection);

        if(attackpoint == null)
        {
            return;
        }
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(attackpoint.position, attackRange);
    }
}
