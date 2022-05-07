using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeScript : MonoBehaviour
{
    // Start is called before the first frame update
    public bool playerInRange;
    public Vector2 playerDetection;
    PlayerInfo playerInfoController;
    PlayerControll playerControllScript;
    public bool isOnGround;
    public bool facingLeft;
    public Animator animator;


    bool attacking;
    bool playAttackAnim;
    public Rigidbody2D rb;
    public float startAttackTimer;
    float attackDelay = 1;

    private float flipHitbox = 1f;
    float startTimer = 0;
    float hitWallTimer = 0;
    float wallAttackTime = 2;
    float animationTime = 2f;



    public LayerMask playerLayer;
    Collider2D[] hitPlayer;

    public bool canAttack = true;

    [Header("Second phase")]
    EnemyHealth enemyHealthScripts;
    int maxHealth; 
    bool secondPhase = false;
    public ParticleSystem poisonRain;
    public Animator camAnim;
    public Transform startTransform;
    Vector3 startPos;



    void Start()
    {
        playerInfoController = GameObject.Find("Player").GetComponent<PlayerInfo>();
        playerControllScript = GameObject.Find("Player").GetComponent<PlayerControll>();
        enemyHealthScripts = GetComponent<EnemyHealth>();
        maxHealth = enemyHealthScripts.health;
        poisonRain.Stop();
        startPos = startTransform.position;
    }

    // Update is called once per frame
    void Update()
    {
        DetectPlayer();

        if (!canAttack)
        {
            startTimer += Time.deltaTime;
            if (startTimer >= startAttackTimer)
            {
                if (facingLeft)
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

        if (enemyHealthScripts.health <= (maxHealth / 2) && !secondPhase)
        {
            playerControllScript.enabled = false;
            animator.SetTrigger("SecondPhase");
            transform.position = startPos;
            secondPhase = true;
            poisonRain.Play();
            camAnim.SetBool("cutscene1", true);
            Invoke("StopCutscene", 1f);
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
                    
                    if (this.transform.position.x > playerInfoController.transform.position.x +10 && !facingLeft) // Armadillo på höger sida av spelare
                    {       
                        characterScale.x *= -1;
                        facingLeft = true;
                        this.transform.localScale = characterScale;     
                        flipHitbox *= -1;       
                    }
                    if (this.transform.position.x < playerInfoController.transform.position.x -10&& facingLeft) // Armadillo på vänster sida av spelaren
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


    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player") && canAttack)
        {
            animator.SetTrigger("ATrigger");
            playerInfoController.TakeDamage(30);
            playerControllScript.Knockback(flipHitbox*100,20);
            canAttack = false;        
        }
        if (other.gameObject.CompareTag("Wall"))
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
                rb.AddForce(new Vector2(-2, 0), ForceMode2D.Impulse);
            }
            else
            {
                rb.AddForce(new Vector2(2, 0), ForceMode2D.Impulse);
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
