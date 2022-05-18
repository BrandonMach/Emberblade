using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class armadilloScript : MonoBehaviour
{
    // Start is called before the first frame update

    public bool playerInRange;
    public Vector2 playerDetection;
    private Vector2 detectionlocation;
    PlayerInfo playerInfoController;
    public bool isOnGround;
    public bool facingLeft;
    public Animator animator;

    float attackTimer;
    float attackStartUpTime;
    bool attacking;
    bool canAttack;
    bool attackmode;
    Rigidbody2D rb;

    BoxCollider2D boxCollider;
    CircleCollider2D circleCollider;
    PlayerControll playerControllScript;
    public Quaternion originalRotationValue;
    int knockBackValue;


    void Start()
    {
        playerInfoController = GameObject.Find("Player").GetComponent<PlayerInfo>();
        playerControllScript = GameObject.Find("Player").GetComponent<PlayerControll>();
        facingLeft = false;
        attackTimer = 0;
        attackStartUpTime = 1;
        attacking = false;
        rb = GetComponent<Rigidbody2D>();
        boxCollider = GetComponent<BoxCollider2D>();
        circleCollider = GetComponent<CircleCollider2D>();
        canAttack = true;
        originalRotationValue = transform.rotation;
    }

    // Update is called once per frame
    void Update()
    {
        detectionlocation = new Vector2(transform.position.x, transform.position.y - 4);

        if (facingLeft)
        {
            knockBackValue = 5;
        }
        else
        {
            knockBackValue = -5;
        }
        DetectPlayer();
        if (attacking && canAttack)
        {          
            animator.SetBool("Attack", true);
            Invoke("StartAttack", attackStartUpTime);
            boxCollider.enabled = false;
            circleCollider.enabled = true;
        }
        if (!canAttack)
        {
            this.transform.rotation = originalRotationValue; // reset rotation
            boxCollider.enabled = true;
            circleCollider.enabled = false;
            
            attackTimer += Time.deltaTime;
            if (attackTimer >= 2)
            {
                canAttack = true;
                attackTimer = 0;
            }
        }

       

    }

    void DetectPlayer()
    {
        Vector3 characterScale = transform.localScale;
        playerInRange = false;

        Collider2D[] hitColliders = Physics2D.OverlapBoxAll(detectionlocation, playerDetection, 0);

        foreach (var colliderHit in hitColliders)
        {
            if (colliderHit.gameObject.CompareTag("Player") && !attackmode)
            {
                playerInRange = true;
                

                if (playerInRange && canAttack)
                {
                    attacking = true;
                    if (this.transform.position.x > playerInfoController.transform.position.x && !facingLeft) // Armadillo p� h�ger sida av spelare
                    {
                        attackmode = true;
                        characterScale.x *= -1;
                        facingLeft = true;
                        this.transform.localScale = characterScale;
                        Debug.Log("Armadillo look left");        
                    }
                    if (this.transform.position.x < playerInfoController.transform.position.x && facingLeft) // Armadillo p� v�nster sida av spelaren
                    {
                        attackmode = true;
                        characterScale.x *= -1;
                        facingLeft = false;
                        this.transform.localScale = characterScale;      
                    }
                }              
            }
            else
            {

            }
        }
    }
    void StartAttack()
    {
        if (canAttack && attacking)
        {
           
            if (facingLeft)
            {
                animator.SetBool("Roll", true);
                rb.AddForce(new Vector2(-1f, 0), ForceMode2D.Impulse);
                //transform.Rotate(new Vector3(0, 0, 2f));
            }
            else
            {
                animator.SetBool("Roll", true);
                rb.AddForce(new Vector2(1f, 0), ForceMode2D.Impulse);
                //transform.Rotate(new Vector3(0, 0, -2f));
            }    
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
           
            animator.SetBool("Roll", false);           
            animator.SetBool("Attack", false);           
            Debug.Log("Rolled Player");
            playerControllScript.Knockback(knockBackValue,3);   
            canAttack = false;
            rb.AddForce(new Vector2(knockBackValue, 0), ForceMode2D.Impulse);
            attacking = false;
            attackmode = false;
        }
        if (collision.gameObject.CompareTag("Wall") || collision.gameObject.CompareTag("Enemy") || collision.gameObject.CompareTag("Breakable"))
        {
            animator.SetBool("Roll", false);
            animator.SetBool("Attack", false);
            rb.AddForce(new Vector2(knockBackValue, 0), ForceMode2D.Impulse);
            attacking = false;
            canAttack = false;
            attackmode = false;
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(detectionlocation, playerDetection);
    }

}
