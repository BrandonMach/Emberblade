using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingSpike : MonoBehaviour
{
    Rigidbody2D rb;
    BoxCollider2D collider;
    public float distance;
    bool isFalling = false;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        collider = GetComponent<BoxCollider2D>();
    }


    void Update()
    {
        Physics2D.queriesStartInColliders = false;
        if (!isFalling)
        {
            RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, distance);

            Debug.DrawRay(transform.position, Vector2.down * distance, Color.red);

            if (hit.transform != null)
            {
                if (hit.transform.tag == "Player")
                {
                    rb.gravityScale = 5;
                    isFalling = true;
                }
            }
        }

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            GameObject.Destroy(this.gameObject);
            Debug.Log("Spikes");
        }

        if (collision.gameObject.CompareTag("Player"))
        {
            GameObject player = collision.gameObject;
            player.GetComponent<PlayerInfo>().TakeDamage(75);
        }
    }

}
