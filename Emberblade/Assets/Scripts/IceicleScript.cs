using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceicleScript : MonoBehaviour
{
    private LineRenderer lineRenderer;
    private Rigidbody2D rb;
    public BoxCollider2D boxColllider;
    public Transform hitPos;
    bool playerDetected;
    public LayerMask playerLayer;
    Vector3 originalPos;
    bool falling;
    // Start is called before the first frame update
    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        rb = GetComponent<Rigidbody2D>();
        boxColllider = GetComponent<BoxCollider2D>();
        originalPos = transform.position;
        lineRenderer.useWorldSpace = true;
        lineRenderer.enabled = true; boxColllider.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit2D playerDetectionLaser = Physics2D.Raycast(transform.position, -transform.up); //Skjuter den detection laser nedåt.
        hitPos.position = playerDetectionLaser.point;

        lineRenderer.SetPosition(0, transform.position); // Laserns starposition 0 
        lineRenderer.SetPosition(1, hitPos.position); // laserns slutposition 1
           
        if (playerDetectionLaser.collider.gameObject.CompareTag("Player")&& !falling)
        {
            Debug.Log("Icicle fall");
            rb.gravityScale = 2;
            boxColllider.enabled = true; // sätter på BoxCollider för att kunna detectera marken elelr spelaren
            falling = true;          
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground")&& falling)
        {
            rb.gravityScale = 0;
            transform.position = originalPos;
            boxColllider.enabled = false;
            falling = false;
        }
        if (collision.gameObject.CompareTag("Player") && falling)
        {
            rb.gravityScale = 0;
            transform.position = originalPos;
            boxColllider.enabled = false;
            falling = false;
        }
    }
}
