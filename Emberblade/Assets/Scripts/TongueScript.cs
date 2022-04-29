using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TongueScript : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject[] tonguePrefab;
    public GameObject player;
    public Vector3 mouthOffset;
    Vector3 playerMounthPos;
    int testInt;
    private Rigidbody2D tongue_Rb;

    void Start()
    {
        tongue_Rb = GetComponent<Rigidbody2D>();
          

    }

    // Update is called once per frame
    void Update()
    {
       
            tongue_Rb.AddForce(Vector2.right,  ForceMode2D.Impulse);
        

    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Wall"))
        {
            Destroy(gameObject);
            
        }
      
    }
}
