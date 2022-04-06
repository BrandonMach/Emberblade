using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    // Start is called before the first frame update

    public bool playerIsNear;
    public float radiusArea;
    public float playerDetectionX;
    public float playerDetectionY;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        playerIsNear = false;

        Collider2D[] hitColliders = Physics2D.OverlapBoxAll(transform.position, new Vector2(playerDetectionX, playerDetectionY),0);

        foreach (var colliderHit in hitColliders)
        {
            if (colliderHit.gameObject.CompareTag("Player"))
            {
                playerIsNear = true;
            }
        }

        if (playerIsNear)
        {
            Debug.Log("Player is in range");
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position, new Vector3(playerDetectionX, playerDetectionY, 1));
    }
}
