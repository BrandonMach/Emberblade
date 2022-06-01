using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class PlatypusBossScript : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] float stopTime = 0.5f;
    [SerializeField] float dropForce;
    [SerializeField] float gravityScale;
   

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

    [Header("Second phase")]
    EnemyHealth enemyHealthScripts;
    int maxHealth;
    bool secondPhase = false;
    public GameObject iciclePrefab;
    public float spawnIcicleLeft;
    public float spawnIcicleRight;
    public float spawnIcicleTop;
    List<float> positionSpawned = new List<float>();

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        enemyHealthScripts = GetComponent<EnemyHealth>();
        playerControllScript = GameObject.Find("Player").GetComponent<PlayerControll>();
        maxHealth = enemyHealthScripts.health;

    }

    // Update is called once per frame
    void Update()
    {
        findPlayerObject.transform.position = player.transform.position + new Vector3(0, 72, 0);
        if (findPlayerObject.transform.position.y > 73)
        {
            findPlayerObject.transform.position = new Vector3(player.transform.position.x, 75, player.transform.position.z); //Max höjd för ground pound
        }

        DetectPlayer();

        //if (Input.GetKeyDown(KeyCode.Alpha5))
        //{
        //    if (!isOnGround)
        //    {
        //        doGroundPound = true;
        //        animator.SetBool("GroundPound", true);
        //    }
        //}

        //if (Input.GetKeyDown(KeyCode.Alpha4))
        //{
        //    startGPAttack = true;          
        //}
        if (startGPAttack)
        {

            transform.position = Vector3.MoveTowards(transform.position, findPlayerObject.transform.position, 100 * Time.deltaTime);
            isOnGround = false;
            if (transform.position.y >= 75)
            {

                startGPAttack = false;
                if (!isOnGround)
                {
                    doGroundPound = true;
                    animator.SetBool("GroundPound", true);
                }

            }
        }


        

        if (enemyHealthScripts.health <= (maxHealth / 2) && !secondPhase)
        {
            
            secondPhase = true;
        }
        if (enemyHealthScripts.health == 0)
        {
            
        }
    }

    void DetectPlayer()
    {

        Collider2D[] hitColliders = Physics2D.OverlapCircleAll(transform.position, detectionRange);

        foreach (var colliderHit in hitColliders)
        {
            if (colliderHit.gameObject.CompareTag("Player") )
            {
                Debug.Log("Atttack player");
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
        float spawnPosX = UnityEngine.Random.Range(spawnIcicleLeft, spawnIcicleRight);
        positionSpawned.Add(spawnPosX);

       
        Vector3 spawnPos = new Vector3(spawnPosX, spawnIcicleTop, 0); // Kan spawna på samma plats
        Instantiate(iciclePrefab, spawnPos, iciclePrefab.transform.rotation);

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
            playerControllScript.Knockback();
            
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
