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
    public bool isAlive = true;
    public LayerMask layer;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        collider = GetComponent<BoxCollider2D>();
        player = GameObject.Find("Player").GetComponent<PlayerInfo>();
    }


    void Update()
    {
        
        Physics2D.queriesStartInColliders = false;
        if (!isFalling)                                                                                         //Medans spiken sitter fast i taket, s� g�rs en raycas osom v�ntar p� att spelaren g�r igenom den
        {
            RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, distance, layer);
            Physics2D.IgnoreLayerCollision(15,6,true);
            Debug.DrawRay(transform.position, Vector2.down * distance, Color.red);

            if (hit.transform != null)
            {
                if (hit.transform.tag == "Player")                                                              // Hittar spelaren, sl� p� gravitation och l�t spiken falla
                {
                    rb.gravityScale = 10;
                    isFalling = true;
                }
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))                                                          //Om spiken tr�ffar marken tas spiken bort
        {
            GameObject.Destroy(this.gameObject);
            Debug.Log("Spikes");
        }

        if (collision.gameObject.tag == "Player" && isAlive)                                                     // tr�ffas spelaren av spiken tar spelaren skada och tas bort
        {    
                Debug.LogError("Ice");
                Destroy(this.gameObject);
                player.TakeDamage(15);
                isAlive = false;
        }
    }

}
