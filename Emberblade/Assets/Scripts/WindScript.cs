using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindScript : MonoBehaviour //Detta är skrivet av: Axel
{
    Rigidbody2D rb;
    bool affectedByWind = false;
    float velocity = 1f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (affectedByWind)                         //när du är påvärkad av vind, ska du tryckas till höger. Jag har gett det en liten acceleratinseffect. När du kommer ut ur området så återställs vindkraften.
        {
            velocity += Time.deltaTime;
            rb.AddForce(Vector2.right * velocity * 100);
            if (velocity >= 80)
            {
                velocity = 80;
            }
        }
        else
        {
            velocity = 1;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            rb = collision.gameObject.GetComponent<Rigidbody2D>();
            affectedByWind = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            affectedByWind = false;
        }
    }
}
