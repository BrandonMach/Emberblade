using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishEnemyScript : MonoBehaviour
{
    // Start is called before the first frame update

    private Rigidbody2D fish_Rb;
    public bool playerInRange;
    public float dectetionRange;
    public bool underWater;
    public float jumpWaitTimer = 0;
    private float waitTime = 1f;
    public Renderer rend;
    public Vector2 startPos;
    public Vector2 maxHeight;

    void Start()
    {
        fish_Rb = GetComponent<Rigidbody2D>();
        rend = GetComponent<Renderer>();
        startPos = transform.position;
        maxHeight.y = startPos.y + 30.5f;
    }

    // Update is called once per frame
    void Update()
    {
        DetectPlayer();
        if (underWater)
        {
            jumpWaitTimer += Time.deltaTime;
            rend.enabled = false;
           
        }
        if (!underWater)
        {
            jumpWaitTimer = 0;
            rend.enabled = true;
        }
        if(startPos.y > maxHeight.y)
        {
            transform.localScale = new Vector3(-1,1,1);
        }
        
      
    }
    void DetectPlayer()
    {

        playerInRange = false;

        Collider2D[] hitColliders = Physics2D.OverlapCircleAll(transform.position, dectetionRange);

        foreach (var colliderHit in hitColliders)
        {
            if (colliderHit.gameObject.CompareTag("Player") && underWater && jumpWaitTimer > waitTime )
            {
                playerInRange = true;
                underWater = false;

                fish_Rb.velocity = new Vector2(fish_Rb.velocity.x, 50);
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            underWater = true;

        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, dectetionRange);
    }
}
