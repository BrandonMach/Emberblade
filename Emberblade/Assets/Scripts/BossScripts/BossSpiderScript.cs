using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossSpiderScript : MonoBehaviour //Detta är skrivet av: Brandon
{
    // Start is called before the first frame update
    [SerializeField]
    GameObject bulletWeb;
    BossHealth enemyHealthScripts;
    int maxHealth;
 


    float fireRate;
    float nextFire;

    [Header ("WebHook/Laser")]
    bool secondPhase = false;
    WebHookScript webHookScript;
    float onRoofTimer;
    float jumpDownTime;
    Rigidbody2D rb;
    public bool onRoof;

    [Header("Fall Attack")]
    public float detectionRange;
    public PlayerInfo playerInfoScript;
    float playerPosX;
    public Animator animator;
    bool fallAttack = false;
    bool findPlayer = true;

    void Start()
    {
        onRoofTimer = 0;
        jumpDownTime = 7f;
        fireRate = 5f;
        nextFire = Time.time;
        rb = GetComponent<Rigidbody2D>();
        webHookScript = GetComponent<WebHookScript>();
        enemyHealthScripts = GetComponent<BossHealth>();
        maxHealth = enemyHealthScripts.health;
    }

    // Update is called once per frame
    void Update()
    {
        playerPosX = playerInfoScript.transform.position.x;
        if (enemyHealthScripts.health <= (maxHealth/2) && !secondPhase)  //Spindeln har minder HP hälften hp kvar
        {
            secondPhase = true;
            rb.gravityScale *= -1;     
            webHookScript.ShootHookWeb();
            
        }
        if (onRoof)                                                                         //Om spindeln är på taket 
        {
            animator.SetBool("Move", true);
            onRoofTimer += Time.deltaTime;
            transform.localScale = new Vector3(transform.localScale.x, -1, transform.localScale.z);  //Vänder Spindeln upp och ner
                                                                                                        //Spider dyker ner 
            if (onRoofTimer >= jumpDownTime)                                          
            {
                animator.SetBool("Move", false);
                animator.SetBool("FallAttack", true);
                onRoof = false;
                rb.gravityScale *= -1;
                onRoofTimer = 0;
                fallAttack = true;
                webHookScript.ShootHookWeb();
            }
        }
        else
        {
            transform.localScale = new Vector3(transform.localScale.x, 1, transform.localScale.z);
        }
        
        CheckIfTimeToFire();
        DetectPlayer();
       
    }
    void DetectPlayer()
    {
        Collider2D[] hitColliders = Physics2D.OverlapCircleAll(transform.position, detectionRange);

        foreach (var colliderHit in hitColliders)
        {
            if (colliderHit.gameObject.CompareTag("Player") && onRoof && findPlayer)
            {
                Debug.Log("Spider Move to player");
                transform.position = new Vector3(playerPosX, transform.position.y, transform.position.z);  //När spindeln är på taket letar den efter spelarens position
            }
        }
    }

    void CheckIfTimeToFire()
    {
        if (Time.time > nextFire)  // en timer för när spindeln för instantiera ett spindelnät igen
        {
            Instantiate(bulletWeb, transform.position + new Vector3(-10,0,0), Quaternion.identity);
            nextFire = Time.time + fireRate;
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Roof")) // Om sindeln kolliderara med taket ska den sluta sjkuta spindelnätet som "drar upp" spindeln
        {
            onRoof = true;
            findPlayer = true;
            webHookScript.StopShootHookWeb();
            
        }

        if (collision.gameObject.CompareTag("Ground") && fallAttack) //Om spindeln kollidearar med marken och dyker ner mot splaren ska spindelnätet som "håller upp" spindeln synas och spindeln ska åka nedåt
        {
            webHookScript.ShootHookWeb();
            onRoof = true;
            fallAttack = false;
            rb.gravityScale *= -1;
            findPlayer = false;
            
        }
        if ( collision.gameObject.CompareTag("Player") && fallAttack) //Om spindeln kollidearar med marken och dyker ner mot splaren ska spindelnätet som "håller upp" spindeln synas och spindeln ska åka nedåt, spelaren ska ockås ta skada
        {
            webHookScript.ShootHookWeb();
            onRoof = true;
            fallAttack = false;
            rb.gravityScale *= -1;
            findPlayer = false;
            playerInfoScript.TakeDamage(100);
            

        }

    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, detectionRange);
    }

}
