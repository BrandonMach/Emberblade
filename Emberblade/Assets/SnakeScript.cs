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
    public Vector2 biteSize;
    public float biteOffsetX;
    public float biteOffsetY;

    Collider2D[] attackRange;


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
        AttackPlayer();


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
         attackRange = Physics2D.OverlapBoxAll(transform.position + new Vector3(biteOffsetX * flipHitbox, biteOffsetY, 0), biteSize, 0);

        foreach (var colliderHit in attackRange)
        {
            if (colliderHit.gameObject.CompareTag("Player"))
            {
                animator.SetTrigger("ATrigger");
                attacking = true;


                if (playAttackAnim)
                {
                  
                    startTimeAttackTimer += Time.deltaTime;
                    if (startTimeAttackTimer >= attackDelay)
                    {
                        
                        startTimeAttackTimer = 0;
                        playerInfoController.TakeDamage(5);
                        playAttackAnim = false;
                    }
                }
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        
    }

    void StartAttack()
    {
       
            if (facingLeft)
            {

                rb.AddForce(new Vector2(-8, 0), ForceMode2D.Impulse);
                
            }
            else
            {
                rb.AddForce(new Vector2(8, 0), ForceMode2D.Impulse);
               
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

        Gizmos.color = Color.blue;
        Gizmos.DrawWireCube(transform.position + new Vector3(biteOffsetX * flipHitbox, biteOffsetY, 0), biteSize);
    }
}
