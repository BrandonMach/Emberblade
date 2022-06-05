using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WebProjectileScript : MonoBehaviour //Detta är skrivet av: Brandon
{
    // Start is called before the first frame update
    float moveSpeed = 20f;
    Rigidbody2D rb;
    GameObject player;
    PlayerInfo playerInforScript;
    Vector2 moveDirection;


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player");
        playerInforScript = player.GetComponent<PlayerInfo>();
        moveDirection = (player.transform.position - transform.position).normalized * moveSpeed; //Sikta mot spelarens position genom att hitta vektorn mellan spelaren och spindeln
        rb.velocity = new Vector2(moveDirection.x, moveDirection.y);
       // Destroy(gameObject, 3f);

    }

    void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.gameObject.CompareTag("Player"))                  //Om spindelnät kolliderar med spelaren ska spelaren skades och förstöras
        {
            Debug.Log("Player Webbed");
            playerInforScript.TakeDamage(40);
            Destroy(gameObject);
        }
        if (collision.gameObject.CompareTag("Wall")|| collision.gameObject.CompareTag("Ground")|| collision.gameObject.CompareTag("Roof"))   //Om spindelnät kolliderar med object med taggen Wall, Ground, Roof ska spelaren skades och förstöras
        {
            Debug.Log("Hit wall Webbed");
            Destroy(gameObject);
        }
        
    }
}
