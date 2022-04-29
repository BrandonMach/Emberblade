using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolAI : MonoBehaviour
{
    // Denna Script används för enemies som bee som patrollar runt till spelaren är i range för att attackera
    public float speed = 10f;
    public float range = 50;

    float startingX;
    int dir = 1;
    bool idle;
    

    // Target
    public float playerDetectionX;
    public float playerDetectionY;


    void Start()
    {
        idle = true;
        startingX = transform.position.x;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (idle)
        {
            Idle();
        }
        else
        {
            Attack();
        }
        
    }

    public void Attack()
    {


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

    public void Idle()
    {
        transform.Translate(Vector2.left * speed * Time.deltaTime * dir);
        if (transform.position.x < startingX || transform.position.x > startingX + range)
        {
            dir *= -1;
            Flip();
        }
    }

    public void Flip()
    {
        transform.localScale = new Vector2(transform.localScale.x * -1, transform.localScale.y);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position, new Vector3(playerDetectionX, playerDetectionY, 1));
    }
}
