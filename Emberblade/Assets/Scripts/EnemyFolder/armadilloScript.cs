using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class armadilloScript : MonoBehaviour
{
    // Start is called before the first frame update

    public bool playerInRange;
    public Vector2 playerDetection;
    PlayerInfo playerInfoController;
    public bool isOnGround;
    public bool facingLeft;
    public Animator animator;

    float attackTimer;
    float attackStartUpTime;
    bool attacking;
    bool canAttack;
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
        if (facingLeft)
        {
            knockBackValue = 5;
        }
        else
        {
            knockBackValue = -5;
        }
        DetectPlayer();
        if (attacking)
        {
            animator.SetBool("ArmadilloAttack", true);
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
            animator.SetBool("Attack", false);
            attackTimer += Time.deltaTime;
            if (attackTimer >= attackStartUpTime)
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

        Collider2D[] hitColliders = Physics2D.OverlapBoxAll(transform.position, playerDetection, 0);

        foreach (var colliderHit in hitColliders)
        {
            if (colliderHit.gameObject.CompareTag("Player"))
            {
                playerInRange = true;

                if (playerInRange)
                {
                    attacking = true;
                    if (this.transform.position.x > playerInfoController.transform.position.x && !facingLeft) // Armadillo på höger sida av spelare
                    {
                        characterScale.x *= -1;
                        facingLeft = true;
                        this.transform.localScale = characterScale;
                        Debug.Log("Armadillo look left");        
                    }
                    if (this.transform.position.x < playerInfoController.transform.position.x && facingLeft) // Armadillo på vänster sida av spelaren
                    {
                        characterScale.x *= -1;
                        facingLeft = false;
                        this.transform.localScale = characterScale;      
                    }
                }              
            }
        }
    }

    void ChangeCanAttackTrue()
    {
        canAttack = true;
    }
    void StartAttack()
    {
        if (canAttack && attacking)
        {
            animator.SetBool("Rolling", true);
            if (facingLeft)
            {
                rb.AddForce(new Vector2(-1, 0), ForceMode2D.Impulse);
                transform.Rotate(new Vector3(0, 0, 5));
            }
            else
            {
                rb.AddForce(new Vector2(1, 0), ForceMode2D.Impulse);
                transform.Rotate(new Vector3(0, 0, -5));
            }    
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {     
            Debug.Log("Rolled Player");
            playerControllScript.Knockback(knockBackValue);   
            canAttack = false;
            rb.AddForce(new Vector2(knockBackValue, 0), ForceMode2D.Impulse);
            attacking = false;
            Invoke("ChangeCanAttackTrue", 5);
            
        }
        if (collision.gameObject.CompareTag("Wall"))
        {                      
            rb.AddForce(new Vector2(knockBackValue, 0), ForceMode2D.Impulse);
            Invoke("ChangeCanAttackTrue", 5);
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position, playerDetection);
    }

}
