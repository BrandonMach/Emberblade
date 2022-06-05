using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceicleScript : MonoBehaviour //Detta är skrivet av: Brandon, används inte
{
    private LineRenderer lineRenderer;
    private Rigidbody2D rb;
    BoxCollider2D boxColllider;
    [SerializeField] Transform hitPos;
    bool playerDetected;
    [SerializeField] LayerMask playerLayer;
    Vector3 originalPos;
    [SerializeField] bool falling;
    [SerializeField] int gravityScaling;


    bool test;

    

    // Start is called before the first frame update
    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        rb = GetComponent<Rigidbody2D>();
        boxColllider = GetComponent<BoxCollider2D>();
        originalPos = transform.position;
        lineRenderer.useWorldSpace = true;
        lineRenderer.enabled = true;
        boxColllider.enabled = false;
        test = true;
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit2D playerDetectionLaser = Physics2D.Raycast(transform.position, -transform.up); //Skjuter den detection laser nedåt.
        hitPos.position = playerDetectionLaser.point;

      
        lineRenderer.SetPosition(0, transform.position); // Laserns starposition 0 
        lineRenderer.SetPosition(1, hitPos.position); // laserns slutposition 1


      
        if (playerDetectionLaser.collider.gameObject.CompareTag("Player") && !falling && test)
        {
            Debug.Log("Icicle fall");
            falling = true;    
            boxColllider.enabled = true;
        }
        if (falling)
        {
            rb.gravityScale = gravityScaling;
            test = false;
        }
        
        
    }



    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            rb.gravityScale = 0;
            transform.position = originalPos;
            boxColllider.enabled = false;
            test = false;
        }
        if (collision.gameObject.CompareTag("Player"))
        {
            rb.gravityScale = 0;
            transform.position = originalPos;
            boxColllider.enabled = false;
            test = false;
            
        }
    }
}
