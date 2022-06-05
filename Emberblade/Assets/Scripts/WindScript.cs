using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindScript : MonoBehaviour //Detta �r skrivet av: Axel
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
        if (affectedByWind)                         //n�r du �r p�v�rkad av vind, ska du tryckas till h�ger. Jag har gett det en liten acceleratinseffect. N�r du kommer ut ur omr�det s� �terst�lls vindkraften.
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
