using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeScript : MonoBehaviour //Detta är skrivet av: Brandon
{
    
    [SerializeField] bool playerInRange;
    [SerializeField] Vector2 playerDetection;
    PlayerInfo playerInfoController;
    PlayerControll playerControllScript;
    [SerializeField] bool isOnGround;
    [SerializeField] bool facingLeft;
    [SerializeField] Animator animator;


    [SerializeField] Rigidbody2D rb;
    [SerializeField] float startAttackTimer;
    

    private float flipHitbox = 1f;
    float startTimer = 0;
    float hitWallTimer = 0;
    float wallAttackTime = 2;

    [SerializeField] LayerMask playerLayer;

    [SerializeField] bool canAttack = true;

    [Header("Second phase")]
    BossHealth enemyHealthScripts;
    int maxHealth; 
    bool secondPhase = false;
    [SerializeField] ParticleSystem poisonRain;
    [SerializeField] Animator camAnim;
    [SerializeField] Transform startTransform;
    Vector3 startPos;



    void Start()
    {
        playerInfoController = GameObject.Find("Player").GetComponent<PlayerInfo>();
        playerControllScript = GameObject.Find("Player").GetComponent<PlayerControll>();
        enemyHealthScripts = GetComponent<BossHealth>();
        maxHealth = enemyHealthScripts.health;
        poisonRain.Stop();
        startPos = startTransform.position;
    }

    // Update is called once per frame
    void Update()
    {
        DetectPlayer();

        if (!canAttack)  //Stoppar Ormen från atta attackera om och om igen 
        {
            startTimer += Time.deltaTime;
            if (startTimer >= startAttackTimer)
            {
                if (facingLeft)                                                 //Om spelaren är till vänster om ormen
                {
                    rb.AddForce(new Vector2(1.005f, 0), ForceMode2D.Impulse);
                }
                else
                {
                    rb.AddForce(new Vector2(-1.005f, 0), ForceMode2D.Impulse);
                }
                canAttack = true;
                startTimer = 0;
            }
        }

        if (enemyHealthScripts.health <= (maxHealth / 2) && !secondPhase)         // när Ormens HP är hälften av max hp startas andra fasen, Starta gift regnet
        {
            playerControllScript.enabled = false;
            animator.SetTrigger("SecondPhase");
            transform.position = startPos;
            secondPhase = true;
            poisonRain.Play();
            camAnim.SetBool("cutscene1", true);
            Invoke("StopCutscene", 1f);
        }
        if(enemyHealthScripts.health == 0)
        {
            poisonRain.Stop();
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
                    
                    if (this.transform.position.x > playerInfoController.transform.position.x + 10 && !facingLeft) // Ormen är på höger sida av spelare
                    {       
                        characterScale.x *= -1;
                        facingLeft = true;
                        this.transform.localScale = characterScale;     
                        flipHitbox *= -1;
                        playerControllScript.knockFromRight = true;
                    }
                    if (this.transform.position.x < playerInfoController.transform.position.x - 10 && facingLeft) // Ormen är på vänster sida av spelaren
                    {
                        characterScale.x *= -1;
                        facingLeft = false;
                        this.transform.localScale = characterScale;
                        flipHitbox *= -1;
                        playerControllScript.knockFromRight = false;
                    }
                    
                    StartAttack();
                }
            }
            
        }
    }


    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player") && canAttack) //Om Ormen kan attackera spelaren och kolliderar med spalren skadas spelaren
        {
            animator.SetTrigger("ATrigger");
            playerInfoController.TakeDamage(30);
            playerControllScript.Knockback(5, 5);
            canAttack = false;        
        }
        if (other.gameObject.CompareTag("Wall")) // Om ormen kolliderara med objekt taggat Wall, kan ormen inte attackera en stund
        {
            canAttack = false;
          
            hitWallTimer += Time.deltaTime;
            if (hitWallTimer >= wallAttackTime)
            {
               canAttack = true;

            }
        }
    }
    

    void StartAttack()
    {
        if (canAttack)
        {
            if (facingLeft)
            {
                rb.AddForce(new Vector2(-10, 0), ForceMode2D.Impulse);
            }
            else
            {
                rb.AddForce(new Vector2(10, 0), ForceMode2D.Impulse);
            }
        }
    }

    void StopCutscene()
    {
        playerControllScript.enabled = true;
        camAnim.SetBool("cutscene1", false);
    }
   

    private void OnDrawGizmosSelected()
    {        
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position, playerDetection);      
    }
}
