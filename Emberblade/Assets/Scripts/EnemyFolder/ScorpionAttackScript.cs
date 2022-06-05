using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScorpionAttackScript : MonoBehaviour //Detta är skrivet av: Brandon
{
    // Start is called before the first frame update
    [Header("Attack Range")]
    [SerializeField] bool playerInRange;
    [SerializeField] float playerInRangeX;
    [SerializeField] float playerInRangeY;
    [SerializeField] bool attackPlayer;
    [Header("Player in Range")]
    [SerializeField] float radiusArea;
    [SerializeField] float playerDetectionX;
    [SerializeField] float playerDetectionY;

    [SerializeField] float moveSpeed = 10;

    private PlayerInfo playerInfo;
    private PlayerControll playerController;
    public Animator animator;
    [Header("Attack Time")]
    private float startTimeAttackTimer;
    [SerializeField] float attackDelay = 2f;
    [Header("Change Sprite")]
    private float flipHitbox = 1;
    private bool facingLeft;
    private Vector3 playerTransformOffest;

    [SerializeField] bool isOnGround;
  



    void Start()
    {
        playerInfo = GameObject.Find("Player").GetComponent<PlayerInfo>();
        playerController = GameObject.Find("Player").GetComponent<PlayerControll>();
        facingLeft = true;
        playerTransformOffest = new Vector3(0, 3.03f, 0);
    }

    // Update is called once per frame
    void Update()
    {
        DetectPlayer();
        AttackPlayer();
        if (playerInRange)
        {
            Debug.Log("Player is in range");
        }
        if (attackPlayer)
        {
            Debug.Log("Attack Player");
        }
        if (!attackPlayer)
        {
            animator.SetBool("Attacking", false);
            startTimeAttackTimer = 0;
        }
        
       


        EnemyFacingPlayer();
      


    }

    void DetectPlayer()
    {
 
        playerInRange = false;

        Collider2D[] hitColliders = Physics2D.OverlapBoxAll(transform.position, new Vector2(playerDetectionX, playerDetectionY), 0);

        foreach (var colliderHit in hitColliders)
        {
            if (colliderHit.gameObject.CompareTag("Player"))
            {
                playerInRange = true;

                if (playerInRange && !attackPlayer)
                {
                    transform.position = Vector3.MoveTowards(transform.position, playerInfo.transform.position - playerTransformOffest, moveSpeed * Time.deltaTime);    // Offset för att fiener inte ska gå mot spelarens mage men istället mot fötterna.            
                }
            } 
        }
    }
    void AttackPlayer()
    {
        attackPlayer = false;
        
        Collider2D[] attackRange = Physics2D.OverlapBoxAll(transform.position + new Vector3(-5 * flipHitbox, -1, 0), new Vector2(playerInRangeX, playerInRangeY), 0);

        foreach (var colliderHit in attackRange)
        {
            if (colliderHit.gameObject.CompareTag("Player"))
            {
                
                attackPlayer = true;
                
                animator.SetBool("Attacking", true);
                startTimeAttackTimer += Time.deltaTime;
                if(transform.position.x < colliderHit.transform.position.x)
                {
                    playerController.knockFromRight = false;
                }
                else
                {
                    playerController.knockFromRight = true;
                }
               
                if (startTimeAttackTimer >= attackDelay && attackPlayer)
                {
                    playerController.Knockback(5, 5);
                    playerInfo.TakeDamage(25);
                    
                    startTimeAttackTimer = 0;
                    attackPlayer = false;
                }          
            }     
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            playerController.Knockback(5, 5);
            playerInfo.TakeDamage(20);
        }
    }

    void EnemyFacingPlayer()
    {
        Vector3 charecterScale = transform.localScale;

        if (playerInfo.transform.position.x > this.transform.position.x && facingLeft)
        {
            flipHitbox *= -1;
            charecterScale.x *= -1;
            this.transform.localScale = charecterScale;
            facingLeft = false;
            Debug.Log("Flip Sprite");
        }
        if (playerInfo.transform.position.x < this.transform.position.x && !facingLeft) // Om player transform är större än enemy vänd på enemy
        {
            flipHitbox *= -1;
            charecterScale.x *= -1;
            this.transform.localScale = charecterScale;
            facingLeft = true;
            Debug.Log("Flip Sprite");
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position, new Vector3(playerDetectionX, playerDetectionY, 1));
        Gizmos.color = Color.blue;
        Gizmos.DrawWireCube(transform.position + new Vector3(-5 * flipHitbox, -1, 0), new Vector3(playerInRangeX, playerInRangeY, 1));
    }
}
