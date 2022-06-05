using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBulletScript : MonoBehaviour //Detta är skrivet av: Brandon + Serena
{
    // Start is called before the first frame update

   [HideInInspector] public float direction;
    private Rigidbody2D tongue_Rb;
    float lifespan;
    void Start()
    {
        tongue_Rb = GetComponent<Rigidbody2D>();
          

    }

    // Update is called once per frame
    void Update()
    {      
            tongue_Rb.AddForce(new Vector2(direction,0), ForceMode2D.Impulse);
    }
    
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Wall"))
        {
            Destroy(gameObject);
            
        }
       if (collision.gameObject.CompareTag("Player"))
        {
            Destroy(gameObject);
            
        }
      
    }
}
