using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScorpionAttackScript : MonoBehaviour
{
    // Start is called before the first frame update

    public bool playerInRange;
    public bool attackPlayer;
    public float radiusArea;
    public float playerDetectionX;
    public float playerDetectionY;
    public float playerInRangeX;
    public float playerInRangeY;
    public float moveSpeed = 10;

    private PlayerInfo playerInfoController;
    public Animator animator;

    private float startTimeAttackTimer;
    public float attackDelay = 2f;

    private float flipHitbox = 1;
    private bool facingLeft;
    private Vector3 playerTransformOffest;

    public bool isOnGround;



    void Start()
    {
        playerInfoController = GameObject.Find("Player").GetComponent<PlayerInfo>();
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
                    transform.position = Vector3.MoveTowards(transform.position, playerInfoController.transform.position - playerTransformOffest, moveSpeed * Time.deltaTime);    // Offset för att fiener inte ska gå mot spelarens mage men istället mot fötterna.            
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

                if(startTimeAttackTimer >= attackDelay && attackPlayer)
                {
                    playerInfoController.TakeDamage(5);
                    startTimeAttackTimer = 0;
                    attackPlayer = false;
                }          
            }     
        }
    }

    void EnemyFacingPlayer()
    {
        Vector3 charecterScale = transform.localScale;
       // Debug.Log("PlayerPos" +playerInfoController.transform.position);

        if (playerInfoController.transform.position.x > this.transform.position.x && facingLeft)
        {
            flipHitbox *= -1;
            charecterScale.x *= -1;
            this.transform.localScale = charecterScale;
            facingLeft = false;
            Debug.Log("Flip Sprite");
        }
        if (playerInfoController.transform.position.x < this.transform.position.x && !facingLeft) // Om player transform är större än enemy vänd på enemy
        {
            flipHitbox *= -1;
            charecterScale.x *= -1;
            this.transform.localScale = charecterScale;
            facingLeft = true;
            Debug.Log("Flip Sprite");
        }
    }
    // Enemy flyger typ
    //private void OnCollisionEnter2D(Collision2D collision) 
    //{
    //    if (collision.gameObject.CompareTag("Ground"))
    //    {
    //        isOnGround = true;


    //    }
        
    //}

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position, new Vector3(playerDetectionX, playerDetectionY, 1));
        Gizmos.color = Color.blue;
        Gizmos.DrawWireCube(transform.position + new Vector3(-5 * flipHitbox, -1, 0), new Vector3(playerInRangeX, playerInRangeY, 1));
    }
}
