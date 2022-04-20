using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WebProjectileScript : MonoBehaviour
{
    // Start is called before the first frame update
    float moveSpeed = 12f;
    Rigidbody2D rb;
    GameObject player;
    PlayerInfo playerInforScript;
    Vector2 moveDiraction;


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player");
        playerInforScript = player.GetComponent<PlayerInfo>();
        moveDiraction = (player.transform.position - transform.position).normalized * moveSpeed;
        rb.velocity = new Vector2(moveDiraction.x, moveDiraction.y);
       // Destroy(gameObject, 3f);

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        //if (collision.gameObject.name.Equals("Player"))
        //{
        //    Destroy(gameObject);
        //}
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("Player Webbed");
            playerInforScript.TakeDamage(5);
            Destroy(gameObject);
        }
        if (collision.gameObject.CompareTag("Wall"))
        {
            Debug.Log("Hit wall Webbed");
            Destroy(gameObject);
        }
        if (collision.gameObject.CompareTag("Ground"))
        {
            Debug.Log("Hit Ground Webbed");
            Destroy(gameObject);
        }
        if (collision.gameObject.CompareTag("Roof"))
        {
            Debug.Log("Hit Roof Webbed");
            Destroy(gameObject);
        }
    }
}
