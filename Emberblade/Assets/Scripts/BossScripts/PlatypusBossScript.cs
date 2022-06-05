using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class PlatypusBossScript : MonoBehaviour //Detta är skrivet av: Brandon
{
    // Start is called before the first frame update
    [SerializeField] float stopTime = 0.5f;
    [SerializeField] float dropForce;
    [SerializeField] float gravityScale;
    public GameObject iceWall;
    private bool facingLeft;

    [Header("Ground Pound")]
    public float jumpSpeed = 3;
    public bool isOnGround;
    public bool doGroundPound = false;
    public bool startGPAttack;
    public Animator animator;
    private Rigidbody2D rb;
    public GameObject icebergPrefab;
    public GameObject findPlayerObject;
    public PlayerInfo player;
    private PlayerControll playerControllScript;
    [SerializeField] float detectionRange;
    [SerializeField] bool canAttack = true;
    [SerializeField] float canAttackTimer = 0;
    private float canAttackDelay = 1.5f;
    [SerializeField] bool stopTrackingPlayer;

    [Header("Second phase")]
    BossHealth enemyHealthScripts;
    int maxHealth;
    bool secondPhase = false;
    public GameObject iciclePrefab;
    public float spawnIcicleLeft;
    public float spawnIcicleRight;
    public float spawnIcicleTop;
    List<float> positionSpawned = new List<float>();
    List<GameObject> icicles = new List<GameObject>();
    FallingSpike fallingSpikeScript;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        enemyHealthScripts = GetComponent<BossHealth>();
        playerControllScript = GameObject.Find("Player").GetComponent<PlayerControll>();
        maxHealth = enemyHealthScripts.health;
        iceWall.SetActive(true);
        animator = GetComponent<Animator>();

    }

    // Update is called once per frame
    void Update()
    {
        Vector3 characterScale = transform.localScale;
        findPlayerObject.transform.position = player.transform.position + new Vector3(0, 60, 0); // För att hitta spelarens position
        if (findPlayerObject.transform.position.y > 73 && !stopTrackingPlayer)
        {
            findPlayerObject.transform.position = new Vector3(player.transform.position.x, 75, player.transform.position.z); //Max höjd för ground pound
        }


        DetectPlayer();
        

        if(playerControllScript.transform.position.x > this.transform.position.x && facingLeft)
        {
            characterScale.x *= -1;
            this.transform.localScale = characterScale;
            facingLeft = false;
        }
        if (playerControllScript.transform.position.x < this.transform.position.x && !facingLeft)
        {
            characterScale.x *= -1;
            this.transform.localScale = characterScale;
            facingLeft = true;
        }


        if (isOnGround)                                                 //Om Näbbdjuret är på marken ska den vänta med att attackera igen
        {
            canAttack = false;
            canAttackTimer += Time.deltaTime;

            if (canAttackTimer >= canAttackDelay)
            {
                canAttackTimer = 0;
                stopTrackingPlayer = true;
                canAttack = true;
            }
        }

        if (!isOnGround)
        {
            animator.SetTrigger("GroundPound");
        }
        


        if (startGPAttack && canAttack)                                                     
        {

            transform.position = Vector3.MoveTowards(transform.position, findPlayerObject.transform.position, 100 * Time.deltaTime); // Näbbdjuret ska förflytta sig mot objektet som letar efter spelarens position
            isOnGround = false;
            startGPAttack = false;
            if (transform.position.y >= 75)                                 //När har nått upp till max höjden 
            {
                
                if (!isOnGround)
                {
                    doGroundPound = true;
                }

            }
        }
   
        

        if (enemyHealthScripts.health <= (maxHealth / 2) && !secondPhase) 
        {
            
            secondPhase = true;
        }
        if (enemyHealthScripts.health == 0) // tar bort isväggen som blokckera spalaren från när näbbdjur dör
        {
            iceWall.SetActive(false);
        }
    }

    void DetectPlayer()
    {

        Collider2D[] hitColliders = Physics2D.OverlapCircleAll(transform.position, detectionRange);

        foreach (var colliderHit in hitColliders)
        {
            if (colliderHit.gameObject.CompareTag("Player") )
            {
                Debug.Log("Perry Atttack player");
                startGPAttack = true;
            }
        }
    }

    private void FixedUpdate()
    {
        if (doGroundPound)
        {
            GroundPoundAttack();
            
        }
        doGroundPound = false;

    }

    private void GroundPoundAttack()
    {
        StopAndSpin();
        StartCoroutine("DropAndSmash");       
    }

    private IEnumerator DropAndSmash()
    {
        yield return new WaitForSeconds(stopTime);

        rb.AddForce(Vector2.down * dropForce, ForceMode2D.Impulse);
    }

    private void StopAndSpin()
    {
        ClearForces();
        rb.gravityScale = 0; // prevents from falling down
        
    }

    private void ClearForces()
    {
        rb.velocity = Vector2.zero;
        rb.angularVelocity = 0;
    }

    void SpawnIceberg()  //Instatierar iceberg variabeln 
    {
      
        Instantiate(icebergPrefab, new Vector3(transform.position.x + 10, transform.position.y -3, transform.position.z), icebergPrefab.transform.rotation);     
        Instantiate(icebergPrefab, new Vector3(transform.position.x - 10, transform.position.y-3, transform.position.z), Quaternion.Euler(0f,180f,0));

        if (secondPhase)
        {
            Instantiate(icebergPrefab, new Vector3(transform.position.x + 5, transform.position.y - 3, transform.position.z), icebergPrefab.transform.rotation);
            Instantiate(icebergPrefab, new Vector3(transform.position.x - 5, transform.position.y - 3, transform.position.z), Quaternion.Euler(0f, 180f, 0));
        }
        

    }

    void SpawnIcicle()  
    {
        if (icicles.Count <= 10)
        {
             float spawnPosX = UnityEngine.Random.Range(spawnIcicleLeft, spawnIcicleRight); //Instatierar istappor inom ett specifkt område
             positionSpawned.Add(spawnPosX);

             Vector3 spawnPos = new Vector3(spawnPosX, spawnIcicleTop, 0); // Kan spawna på samma plats
             Instantiate(iciclePrefab, spawnPos, iciclePrefab.transform.rotation);
             icicles.Add(iciclePrefab);
           
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            isOnGround = true;
            animator.SetBool("GroundPound", false);
            if (secondPhase)                            //Om näbbdjur är i sin andra fas ohccolliderar med marken ska istappar skapas
            { 
                InvokeRepeating("SpawnIcicle", 1, 7);
            }
           
        }
        if (other.gameObject.CompareTag("Ground"))  
        {
            SpawnIceberg();
        }

        if( other.contacts[0].normal.y >= 0.5 && other.gameObject.CompareTag("Player"))
        {
            CompleteGroundPound();
            Debug.LogError("Hit Player with ass");
            if (transform.position.x < other.transform.position.x)
            {
                playerControllScript.knockFromRight = false;
            }
            else
            {
                playerControllScript.knockFromRight = true;
            }
            player.TakeDamage(10);
            playerControllScript.Knockback(50, 50);
            
        }
    }

    private void CompleteGroundPound()
    {
        rb.gravityScale = gravityScale;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectionRange);
      
    }
}
