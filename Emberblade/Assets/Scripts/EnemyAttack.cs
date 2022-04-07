using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    // Start is called before the first frame update

    public bool playerIsNear;
    public bool attackPlayer;
    public float radiusArea;
    public float playerDetectionX;
    public float playerDetectionY;
    public float playerInRangeX;
    public float playerInRangeY;
    private float moveSpeed;

    private PlayerInfo playerInfoController;
    public Animator animator;

    private float startTimeAttackTimer;
    private float attackDelay = 1.2f;

    private float flipHitbox = 1;
    private bool facingLeft;

    
    void Start()
    {
        playerInfoController = GameObject.Find("Player").GetComponent<PlayerInfo>();
        moveSpeed = 0.05f;
        facingLeft = true;
    }

    // Update is called once per frame
    void Update()
    {
        DetectPlayer();
        AttackPlayer();
        if (playerIsNear)
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
        
        Debug.Log("Yo" +startTimeAttackTimer);


        EnemyFacingPlayer();
      


    }

    void DetectPlayer()
    {
 
        playerIsNear = false;

        Collider2D[] hitColliders = Physics2D.OverlapBoxAll(transform.position, new Vector2(playerDetectionX, playerDetectionY), 0);

        foreach (var colliderHit in hitColliders)
        {
            if (colliderHit.gameObject.CompareTag("Player"))
            {
                playerIsNear = true;

                if (playerIsNear)
                {
                    transform.position = Vector3.MoveTowards(transform.position, playerInfoController.transform.position, moveSpeed);               
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

                if(startTimeAttackTimer >= attackDelay && colliderHit.gameObject.CompareTag("Player"))
                {
                    playerInfoController.TakeDamage(20);
                    startTimeAttackTimer = 0;
                }          
            }     
        }
    }

    void EnemyFacingPlayer()
    {
        Vector3 charecterScale = transform.localScale;

        if (playerInfoController.transform.position.x > this.transform.position.x && facingLeft)
        {
            flipHitbox *= -1;
            charecterScale.x *= -1;
            transform.localScale = charecterScale;
            facingLeft = false;
        }
        if (playerInfoController.transform.position.x < this.transform.position.x && !facingLeft) // Om player transform är större än enemy vänd på enemy
        {
            flipHitbox *= -1;
            charecterScale.x *= -1;
            transform.localScale = charecterScale;
            facingLeft = true;
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
