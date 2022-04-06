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

    void Start()
    {
        
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
            }
        }
    }
    void AttackPlayer()
    {
        attackPlayer = false;

        Collider2D[] attackRange = Physics2D.OverlapBoxAll(transform.position + new Vector3(-5, -1, 0), new Vector2(playerInRangeX, playerInRangeY), 0);

        foreach (var colliderHit in attackRange)
        {
            if (colliderHit.gameObject.CompareTag("Player"))
            {
                attackPlayer = true;
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position, new Vector3(playerDetectionX, playerDetectionY, 1));
        Gizmos.color = Color.blue;
        Gizmos.DrawWireCube(transform.position + new Vector3(-5, -1, 0), new Vector3(playerInRangeX, playerInRangeY, 1));
    }
}
