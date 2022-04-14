using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishEnemyScript : MonoBehaviour
{
    // Start is called before the first frame update

    private Rigidbody2D fish_Rb;
    public bool playerInRange;
    public float dectetionRange;
    public bool isUnderWater;
    public float jumpWaitTimer = 0;
    private float waitTime = 1f;
    public Renderer rend;
    private Vector2 startPos;
    private Vector2 maxHeight;
    private PlayerInfo playerInfoController;
    public BoxCollider2D boxCollider;

    public int jumpheight = 0;

    void Start()
    {
        fish_Rb = GetComponent<Rigidbody2D>();
        rend = GetComponent<Renderer>();
        playerInfoController = GameObject.Find("Player").GetComponent<PlayerInfo>();
        startPos = transform.position;
        maxHeight.y = startPos.y + 40f;
    }

    // Update is called once per frame
    void Update()
    {
        DetectPlayer();
        DamagePalyer();
        if (isUnderWater)
        {
            jumpWaitTimer += Time.deltaTime;
            rend.enabled = false;
            transform.localScale = new Vector3(1, 1, 1);
           
        }
        if (!isUnderWater)
        {
            jumpWaitTimer = 0;
            rend.enabled = true; 
        }
        Debug.Log("Fish start" + startPos);
        Debug.Log("max jump" + maxHeight);
        if(transform.position.y > maxHeight.y)
        {
            transform.localScale = new Vector3(-1,1,1);
            Debug.Log("Flip flip");
        }
      
    }
    void DetectPlayer()
    {

        playerInRange = false;

        Collider2D[] hitColliders = Physics2D.OverlapCircleAll(transform.position, dectetionRange);

        foreach (var colliderHit in hitColliders)
        {
            if (colliderHit.gameObject.CompareTag("Player") && isUnderWater && jumpWaitTimer > waitTime )
            {
                playerInRange = true;
                isUnderWater = false;

                fish_Rb.velocity = new Vector2(fish_Rb.velocity.x, jumpheight);
            }
        }
    }
    void DamagePalyer()
    {
        Collider2D[] hitColliders = Physics2D.OverlapCircleAll(transform.position, 4);

        foreach (var colliderHit in hitColliders)
        {
            if (colliderHit.gameObject.CompareTag("Player") && playerInfoController.canTakeDamage)
            {
                playerInfoController.TakeDamage(5);
                Debug.Log("aasadasd");
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isUnderWater = true;

        }      
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, dectetionRange);
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, 4);
    }
}
