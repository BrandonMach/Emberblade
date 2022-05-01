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
    bool facingLeft;
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
        
        DetectPlayer();
        if (attacking)
        {
            animator.SetBool("ArmadilloAttack", true);
            Invoke("StartAttack", attackStartUpTime);
            boxCollider.enabled = false;
            circleCollider.enabled = true;
        }
        if (!canAttack)
        {
            this.transform.rotation = originalRotationValue;
            boxCollider.enabled = true;
            circleCollider.enabled = false;
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

    void StartAttack()
    {
        if (canAttack)
        {
            rb.AddForce(new Vector2(-1, 0), ForceMode2D.Impulse);
            animator.SetBool("Rolling", true);
            transform.Rotate(new Vector3(0, 0, 5));
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {     
            Debug.Log("Rolled Player");
            playerControllScript.Knockback();
            canAttack = false;
            rb.AddForce(new Vector2(3, 0), ForceMode2D.Impulse);
            animator.SetBool("Rolling", false);
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position, playerDetection);
    }

}
