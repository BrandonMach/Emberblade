using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShootScript : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject bulletPrefab;
    EnemyBulletScript bulletScript;
    public bool playerInRange;
    public Vector2 playerDetection;
    PlayerInfo playerInfoController;
    float startShot;
    float shootSpeed;
    void Start()
    {
        playerInfoController = GameObject.Find("Player").GetComponent<PlayerInfo>();
        bulletScript = bulletPrefab.GetComponent<EnemyBulletScript>();
        startShot = 0;
        shootSpeed = 1;
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

                if (playerInRange /*&& isOnGround*/)
                {
                    // transform.position = Vector3.MoveTowards(transform.position, playerInfoController.transform.position - playerTransformOffest, moveSpeed);    // Offset för att fiener inte ska gå mot spelarens mage men istället mot fötterna.

                  if(this.transform.position.x > playerInfoController.transform.position.x)
                  {
                        
                        ShootPrefab(-1);

                  }
                  if (this.transform.position.x < playerInfoController.transform.position.x)
                  {
                        ShootPrefab(1);
                  }

                }
            }
        }
    }

   

    void ShootPrefab(int direction)
    {
        startShot += Time.deltaTime;
        if (startShot > shootSpeed)
        {
            Vector2 spawnPos = new Vector2(transform.position.x + direction, transform.position.y);
            bulletScript.direction = direction;
            Instantiate(bulletPrefab, spawnPos, bulletPrefab.transform.rotation);
            startShot = 0;
        }

    
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position, playerDetection);
    }
}
