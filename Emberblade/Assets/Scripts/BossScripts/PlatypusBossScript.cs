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

    }

    // Update is called once per frame
    void Update()
    {
        findPlayerObject.transform.position = player.transform.position + new Vector3(0, 60, 0); // För att hitta spelarens position
        if (findPlayerObject.transform.position.y > 73 && !stopTrackingPlayer)
        {
            findPlayerObject.transform.position = new Vector3(player.transform.position.x, 75, player.transform.position.z); //Max höjd för ground pound
        }

        //if (canAttack)
        //{

        //}
        DetectPlayer();
        

        if (isOnGround)
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
        Debug.LogError(canAttackTimer);

        if (startGPAttack && canAttack)
        {

            transform.position = Vector3.MoveTowards(transform.position, findPlayerObject.transform.position, 100 * Time.deltaTime);
            isOnGround = false;
            startGPAttack = false;
            if (transform.position.y >= 75)
            {         
                if (!isOnGround)
                {
                    doGroundPound = true;
                    animator.SetBool("GroundPound", true);
                }

            }
        }
        Debug.LogError("Amount icicle" + icicles.Count);
        foreach (GameObject icicle in icicles)
        {
            fallingSpikeScript = icicle.GetComponent<FallingSpike>();
            if (!fallingSpikeScript.isAlive)
            {
                icicles.Remove(icicle);
                
            }
        }
        

        if (enemyHealthScripts.health <= (maxHealth / 2) && !secondPhase)
        {
            
            secondPhase = true;
        }
        if (enemyHealthScripts.health == 0)
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

    void SpawnIceberg()
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
             float spawnPosX = UnityEngine.Random.Range(spawnIcicleLeft, spawnIcicleRight);
             positionSpawned.Add(spawnPosX);

             Vector3 spawnPos = new Vector3(spawnPosX, spawnIcicleTop, 0); // Kan spawna på samma plats
             Instantiate(iciclePrefab, spawnPos, iciclePrefab.transform.rotation);
             icicles.Add(iciclePrefab);
            
            Debug.LogError(icicles.Count);
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            isOnGround = true;
            animator.SetBool("GroundPound", false);
            if (secondPhase)
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
