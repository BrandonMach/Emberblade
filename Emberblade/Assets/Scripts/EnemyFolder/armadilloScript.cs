using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class armadilloScript : MonoBehaviour //Detta är skrivet av: Sebastian + Brandon
{
    // Start is called before the first frame update

    [SerializeField] bool playerInRange;
    [SerializeField] Vector2 playerDetection;
    private Vector2 detectionlocation;
    PlayerInfo playerInfoController;
    [SerializeField] bool isOnGround;
    [SerializeField] bool facingLeft;
    [SerializeField] Animator animator;

    float attackTimer;
    float attackStartUpTime;
    bool attacking;
    bool canAttack;
    bool attackmode;
    bool stopMoving;
    Rigidbody2D rb;

    BoxCollider2D boxCollider;
    CircleCollider2D circleCollider;
    PlayerControll playerControllScript;
    [SerializeField] Quaternion originalRotationValue;
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

        if (facingLeft) // Ger Knockback till spelaren beroende på om armidillon är i vänsra sidan eller högra sidan
        {
            knockBackValue = 5;
        }
        else
        {
            knockBackValue = -5;
        }
        DetectPlayer(); 
        if (attacking && canAttack) // Om fienden kan attackera och är redo att attackera
        {          
            animator.SetBool("Attack", true);
            Invoke("StartAttack", attackStartUpTime); //Kör Metoden
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

        foreach (var colliderHit in hitColliders) //Hitta spelaren
        {
            if (colliderHit.gameObject.CompareTag("Player") && !attackmode) // Om Spelaren är inne detection hitboxen
            {
                playerInRange = true;
                

                if (playerInRange && canAttack)
                {
                    attacking = true; // Attackera
                    if (this.transform.position.x > playerInfoController.transform.position.x && !facingLeft) // Armadillo på höger sida av spelare
                    {
                        attackmode = true; // Attackera
                        characterScale.x *= -1;
                        facingLeft = true;
                        this.transform.localScale = characterScale;
                        Debug.Log("Armadillo look left");        
                    }
                    if (this.transform.position.x < playerInfoController.transform.position.x && facingLeft) // Armadillo på vänster sida av spelaren
                    {
                        attackmode = true; // Attackera
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
        if (canAttack && attacking)  // Fienden kan attackera
        {
           
            if (facingLeft) // Om spelaren är till höger om fienden
            {
                animator.SetBool("Roll", true);
                rb.AddForce(new Vector2(-4f, 0), ForceMode2D.Impulse);
                //transform.Rotate(new Vector3(0, 0, 2f));
            }
            else // Om spelaren är till vänster om fienden
            {
                animator.SetBool("Roll", true);
                rb.AddForce(new Vector2(4f, 0), ForceMode2D.Impulse);
                //transform.Rotate(new Vector3(0, 0, -2f));
            }    
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && attacking) //Träffar Spelaren
        {
           
            animator.SetBool("Roll", false);           
            animator.SetBool("Attack", false);           
            Debug.Log("Rolled Player");
            if (transform.position.x < collision.transform.position.x) // Ger Knockback till spelaren beroende på om armidillon är i vänsra sidan eller högra sidan
            {
                playerControllScript.knockFromRight = false;
            }
            else
            {
                playerControllScript.knockFromRight = true;
            }
            playerControllScript.Knockback(30, 15); //Spelaren Knockback
            playerInfoController.TakeDamage(20); //Spelaren Tar Skada
            canAttack = false;
            rb.AddForce(new Vector2(knockBackValue, 0), ForceMode2D.Impulse);
            attacking = false;
            attackmode = false;
        }
        else if (collision.gameObject.CompareTag("Player") && !attacking) // Om Spelaren frivilligt träffar fienden
        {
            playerControllScript.Knockback(10, 5); //Spelaren Knockback
            playerInfoController.TakeDamage(20); //Spelaren Tar Skada
        }
        if (collision.gameObject.CompareTag("Wall") || collision.gameObject.CompareTag("Enemy") || collision.gameObject.CompareTag("Breakable"))        //Om armadillo krockar medobjekt taggad wall, enemy, breakable
        {
            animator.SetBool("Roll", false);
            animator.SetBool("Attack", false);
            rb.AddForce(new Vector2(knockBackValue, 0), ForceMode2D.Impulse);
            attacking = false;
            canAttack = false;
            attackmode = false;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        stopMoving = true;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(detectionlocation, playerDetection); // ritar ut hitboxen i unity 
    }

}
