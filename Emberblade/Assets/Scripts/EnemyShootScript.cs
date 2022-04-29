using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShootScript : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject bulletPrefab;
    public bool playerInRange;
    public Vector2 playerDetection;
    PlayerInfo playerInfoController;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        DetectPlayer();
    }
    void DetectPlayer()
    {

        playerInRange = false;

        Collider2D[] hitColliders = Physics2D.OverlapBoxAll(transform.position, playerDetection, 0);

        foreach (var colliderHit in hitColliders)
        {
            if (colliderHit.gameObject.CompareTag("Player"))
            {
                playerInRange = true;

                if (playerInRange /*&& isOnGround*/) // kan bara jaga n�r dem �r p� Ground
                {
                    // transform.position = Vector3.MoveTowards(transform.position, playerInfoController.transform.position - playerTransformOffest, moveSpeed);    // Offset f�r att fiener inte ska g� mot spelarens mage men ist�llet mot f�tterna.
                  
                        ShootPrefab();
                    
                   

                   

                }
            }
        }
    }

   

    void ShootPrefab()
    {
        Vector2 spawnPos = new Vector2(transform.position.x + 10, transform.position.y);
        Instantiate(bulletPrefab, spawnPos, bulletPrefab.transform.rotation);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position, playerDetection);
    }
}
