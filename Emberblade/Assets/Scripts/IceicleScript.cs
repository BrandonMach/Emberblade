using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceicleScript : MonoBehaviour
{
    //private LineRenderer lineRenderer;
    //private Rigidbody2D rb;
    //BoxCollider2D boxColllider;
    //public Transform hitPos;
    //bool playerDetected;
    public LayerMask playerLayer;
    Vector3 originalPos;
    public bool falling;
    public int gravityScaling;


    public LayerMask groundLayer;
    public bool detectPlayer;
    public float groundLenght;
    public Vector3 colliderOffset;


    // Start is called before the first frame update
    void Start()
    {
        //lineRenderer = GetComponent<LineRenderer>();
        //rb = GetComponent<Rigidbody2D>();
        //boxColllider = GetComponent<BoxCollider2D>();
        //originalPos = transform.position;
        //lineRenderer.useWorldSpace = true;
        //lineRenderer.enabled = true;
        //boxColllider.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        //RaycastHit2D playerDetectionLaser = Physics2D.Raycast(transform.position, -transform.up); //Skjuter den detection laser nedåt.
        //hitPos.position = playerDetectionLaser.point;

      
        //lineRenderer.SetPosition(0, transform.position); // Laserns starposition 0 
        //lineRenderer.SetPosition(1, hitPos.position); // laserns slutposition 1


        detectPlayer = Physics2D.Raycast(transform.position + colliderOffset, Vector2.down, groundLenght, groundLayer);
        if (detectPlayer)
        {
            Debug.Log("Found Player");
        }
        //if (playerDetectionLaser.collider.gameObject.CompareTag("Player") && !falling)
        //{
        //    Debug.Log("Icicle fall");
        //    falling = true;
        //    rb.gravityScale = gravityScaling;
        //    boxColllider.enabled = true; // sätter på BoxCollider för att kunna detectera marken eller spelaren
        //}
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            //rb.gravityScale = 0;
            //transform.position = originalPos;
            //boxColllider.enabled = false;
            //falling = false;
            //lineRenderer.enabled = true;
        }
        if (collision.gameObject.CompareTag("Player"))
        {
            //rb.gravityScale = 0;
            //transform.position = originalPos;
            //boxColllider.enabled = false;
            //falling = false;
            //lineRenderer.enabled = true;
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.white;
        Gizmos.DrawLine(transform.position + colliderOffset, transform.position + colliderOffset + Vector3.down * groundLenght);
       
    }
}
