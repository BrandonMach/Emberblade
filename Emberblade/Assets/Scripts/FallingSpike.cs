using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingSpike : MonoBehaviour //Detta �r skrivet av: Axel och Philip
{
    Rigidbody2D rb;
    BoxCollider2D collider;
    PlayerInfo player;
    [SerializeField] float distance;
    bool isFalling = false;
    bool isAlive = true;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        collider = GetComponent<BoxCollider2D>();
        player = GameObject.Find("Player").GetComponent<PlayerInfo>();
    }


    void Update()
    {
        Physics2D.queriesStartInColliders = false;
        if (!isFalling)                                                                         //Medans spiken sitter fast i taket, s� g�rs en raycast v�ntar p� att hitta en spelare. 
        {
            RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, distance);

            Debug.DrawRay(transform.position, Vector2.down * distance, Color.red);

            if (hit.transform != null)
            {
                if (hit.transform.tag == "Player")                                             //Hittar den en spelare, s� sl� p� gravitation l�t spiken ramla.
                {
                    rb.gravityScale = 5;
                    isFalling = true;
                }
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))                                         //tr�ffar spiken marken, s� ska spiken tas bort.
        {
            GameObject.Destroy(this.gameObject);
            Debug.Log("Spikes");
        }

        if (collision.gameObject.tag =="Player" && isAlive)                                    //tr�ffar spiken en spelare, s� ska spelaren ta skada.
        {    
                Debug.LogError("Ice");
                Destroy(this.gameObject);
                player.TakeDamage(15);
                isAlive = false;
        }
    }

}
