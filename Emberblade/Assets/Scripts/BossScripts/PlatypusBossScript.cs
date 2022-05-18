using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatypusBossScript : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] float stopTime = 0.5f;
    [SerializeField] float dropForce;
    [SerializeField] float gravityScale;

    public bool isOnGround;
    public bool doGroundPound = false;
    public Animator animator;
    private Rigidbody2D rb;
    

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();   
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            if (!isOnGround)
            {
                doGroundPound = true;
                animator.SetBool("GroundPound", true);
            }
        }

        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            rb.AddForce(Vector2.up * 20, ForceMode2D.Impulse);
            isOnGround = false;
        }
    }

    private void FixedUpdate()
    {
        if (doGroundPound)
        {
            GroundPoundAttack();
        }
        doGroundPound = false;

    }

    private void GroundPoundAttack()
    {
        StopAndSpin();
        StartCoroutine("DropAndSmash");

        
    }

    private IEnumerator DropAndSmash()
    {
        yield return new WaitForSeconds(stopTime);

        rb.AddForce(Vector2.down * dropForce, ForceMode2D.Impulse);
    }

    private void StopAndSpin()
    {
        ClearForces();
        rb.gravityScale = 0; // prevents from falling down
    }

    private void ClearForces()
    {
        rb.velocity = Vector2.zero;
        rb.angularVelocity = 0;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            isOnGround = true;
            animator.SetBool("GroundPound", false);
        }

        if( other.contacts[0].normal.y >= 0.5 && other.gameObject.CompareTag("Player"))
        {
            CompleteGroundPound();
            Debug.LogError("Hit Player with ass");
        }
    }

    private void CompleteGroundPound()
    {
        rb.gravityScale = gravityScale;
    }
}
