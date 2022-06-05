using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishEnemyScript : MonoBehaviour //Detta är skrivet av: Brandon
{
    // Start is called before the first frame update

    private Rigidbody2D fish_Rb;
    [SerializeField] bool playerInRange;
    [SerializeField] float detectionRange;
    [SerializeField] bool isUnderWater;
    [SerializeField] float jumpWaitTimer = 0;
    private float waitTime = 1f;
    [SerializeField] Renderer rend;
    private Vector2 startPos;
    private Vector2 maxHeight;
    private PlayerInfo playerInfo;
    PlayerControll playerController;
    [SerializeField] BoxCollider2D boxCollider;

    public int jumpheight = 0;

    void Start()
    {
        fish_Rb = GetComponent<Rigidbody2D>();
        rend = GetComponent<Renderer>();
        playerInfo = GameObject.Find("Player").GetComponent<PlayerInfo>();
        playerController = GameObject.Find("Player").GetComponent<PlayerControll>();
        startPos = transform.position;
        maxHeight.y = startPos.y + 50f;
    }

    // Update is called once per frame
    void Update()
    {
        DetectPlayer();
        DamagePalyer();
        if (isUnderWater)                                 //När fisken är under vatten ska rendern bli oslynlig
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
       
        if(transform.position.y > maxHeight.y)              //När fisken har nått maxhöjd
        {
            transform.localScale = new Vector3(-1,1,1);
            Debug.Log("Flip flip");
        }
      
    }
    void DetectPlayer()
    {

        playerInRange = false;

        Collider2D[] hitColliders = Physics2D.OverlapCircleAll(transform.position, detectionRange);

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
        Collider2D[] hitColliders = Physics2D.OverlapCircleAll(transform.position, 4); //Fish attack hitbox

        foreach (var colliderHit in hitColliders)
        {
            if (colliderHit.gameObject.CompareTag("Player") && playerInfo.canTakeDamage)
            {

                if (transform.position.x < colliderHit.transform.position.x)
                {
                    playerController.knockFromRight = false;
                }
                else
                {
                    playerController.knockFromRight = true;
                }
                playerController.Knockback(5, 5);
                playerInfo.TakeDamage(15);
                boxCollider.isTrigger = true;
                
            }
            if (colliderHit.gameObject.CompareTag("Ground"))
            {
                boxCollider.isTrigger = false;
                isUnderWater = true;

            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectionRange);
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, 4);
    }
}
