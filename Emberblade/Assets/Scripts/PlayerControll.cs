using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerControll : MonoBehaviour
{
    // Start is called before the first frame update
    public float movementSpeed = 1;
    private Rigidbody2D player_Rb;

    public float jumpforce = 1;
    private float originalJumpForce;
    public GameObject jumpRingPrefab;
    public float jRingSpawnTime = 0.1f;
    public GameObject dashEffectPrefab;
    public float dESpawnTime = 0;
    private CapsuleCollider2D collider;
    private BoxCollider2D boxCollider;

    Vector2 oGOffset;
    Vector2 oGSize;
    Vector3 oGpos;

    Vector2 standingBoxOffset;
    Vector2 stadningBoxSize;

    //Jump
    private float jumpTimer = 0;
    private float jumpStallTime = 0;
    public bool falling;
    public float maxJumpHeight;

    //Dash
    public float dashForce;
    public float startDashTimer;
    private float currentDashTime;
    private float dashDirection;

    private bool isDashing;
    private PlayerInfo playerInfoScript;

    UnityEvent landing = new UnityEvent();
    

    public Animator animator;

    //---------------------------------
    [Header ("New Ground detection")]
    public LayerMask groundLayer;
    public bool isOnGround;
    public float groundLenght;
    public Vector3 colliderOffset;

    [Header("New Jump")]
    public float jumpSpeed = 15;
    public float jumpDelay = 0.25f;
    private float lumpTimer = 0;
    //public int jumpCounter = 0;
    public bool canDoubleJump = false;
    public bool hasUnlockedDJ = false;

    [Header("Physics")]
    public float gravity = 1;
    public float fallMultiplier = 5f;
    public float liniearDrag = 4f;

    [Header("Parry")]
    public bool isParrying = false;
    private float parryStart = 0;
    private float parryWindow = 0.5f;

    [Header("Hook")]
    public GrappleHookScript ghScript;
    public LayerMask grapplelayer;

    void Start()
    {
        player_Rb = GetComponent<Rigidbody2D>();
        collider = GetComponent<CapsuleCollider2D>();
        boxCollider = GetComponent<BoxCollider2D>();
        playerInfoScript = GetComponent<PlayerInfo>();
        originalJumpForce = jumpforce;


        oGSize = collider.size;
        oGOffset = collider.offset;

        standingBoxOffset = boxCollider.offset;
        stadningBoxSize = boxCollider.size;

        landing.AddListener(OnLanding);
    }

    // Update is called once per frame
    void Update()
    {
       
        Move();



        Falling();
        Debug.Log("Jump height " + transform.position.y);


        if (!hasUnlockedDJ)
        {
            canDoubleJump = false;
        }

        JumpInput();
        isOnGround = Physics2D.Raycast(transform.position + colliderOffset, Vector2.down, groundLenght, groundLayer) || Physics2D.Raycast(transform.position - colliderOffset, Vector2.down, groundLenght, groundLayer);
       

        if (Input.GetKeyDown(KeyCode.Space) && canDoubleJump && !isOnGround)
        {
            canDoubleJump = false;

        }
        Parry();

    }

    void Parry()
    {
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            parryStart = 0;
            isParrying = true;
        }
        if (isParrying)
        {
            Debug.Log("Parrying");
            animator.SetBool("Parry", true);
            parryStart += Time.deltaTime;
            if (parryStart >= parryWindow)
            {
                isParrying = false;
            }
        }
        if (!isParrying)
        {
            animator.SetBool("Parry", false);
        }
    }
    void Falling()
    {
        if (isOnGround)
        {
            player_Rb.gravityScale = 0;
            //jumpCounter = 0;
            canDoubleJump = true;
            landing.Invoke();
            //animator.SetBool("Jumping", false);
        }
        else
        {
            player_Rb.gravityScale = gravity;
            player_Rb.drag = liniearDrag * 0.15f;
            if (player_Rb.velocity.y < 0)
            {
                player_Rb.gravityScale = gravity * fallMultiplier;
            }
            else if (player_Rb.velocity.y > 0 && !Input.GetButton("Jump")) //Not holding the jump button
            {
                player_Rb.gravityScale = gravity * (fallMultiplier / 2);
            }
        }
    }

    void Move()
    {

        float moveX = Input.GetAxisRaw("Horizontal");
        float moveBy = moveX * movementSpeed; //Variabel borde byta namn
        player_Rb.velocity = new Vector2(moveBy, player_Rb.velocity.y);
        //Flips the sprite

        Debug.Log("aaaah"+ Mathf.Abs(moveBy));
        Vector3 characterScale = transform.localScale;
        if (Input.GetAxisRaw("Horizontal") < 0) //Left
        {
            characterScale.x = -1.45f;
            if (isOnGround)
            {
                PlayRunAnimation();
            }
          
        }
        else
        {
            animator.SetFloat("Speed", 0.0f);
        }
        if (Input.GetAxisRaw("Horizontal") > 0) //Right
        {
            characterScale.x = 1.45f;
            if (isOnGround)
            {
                PlayRunAnimation();
            }
           
        }
      
        transform.localScale = characterScale;

        //Dash
        if (Input.GetKeyDown(KeyCode.RightShift) && moveX != 0 && playerInfoScript.currentEnergy >=20) //Kan bara dasha om man input en direction
        {
            isDashing = true;
            currentDashTime = startDashTimer;
            player_Rb.velocity = Vector2.zero;
            dashDirection = (int)moveX;
            playerInfoScript.UseEnergy(20);
            
        }
        if (isDashing)
        {
            player_Rb.velocity = transform.right * dashDirection * dashForce;
            currentDashTime -= Time.deltaTime;

            if(currentDashTime <= 0)
            {
                isDashing = false;
            }
        }
        

        if (Input.GetKey(KeyCode.C))
        {
            animator.SetBool("Sit", true);
            collider.size = new Vector2(4.577552f, 4.577552f);
            collider.offset = new Vector2(-0.01422455f, -1f);
            movementSpeed = 13;
            jumpforce = 20;
            boxCollider.offset = new Vector2(-0.05918601f, -2.19f);
            boxCollider.size = new Vector2(3.4216f, 5);
        }
        else
        {
            animator.SetBool("Sit", false);
            collider.size = oGSize;
            collider.offset = oGOffset;
            movementSpeed = 25;
            jumpforce = 40;
            boxCollider.offset = standingBoxOffset;
            boxCollider.size = stadningBoxSize;
        }

    }
    void PlayRunAnimation()
    {
        animator.SetFloat("Speed", 2);
    }

    void JumpInput()
    {
       

        if (Input.GetKeyDown(KeyCode.Space) && isOnGround || Input.GetKeyDown(KeyCode.Space)  && canDoubleJump)
        {
            animator.SetBool("Jumping", true);
                Jumping();
                //jumpCounter ++;
            
                //Debug.Log("dsdsa" + jumpCounter);
        }       
    }
    void Jumping()
    {
        player_Rb.velocity = new Vector2(player_Rb.velocity.x, 0);
        player_Rb.AddForce(Vector2.up * jumpSpeed, ForceMode2D.Impulse);
        jumpTimer = 0;
    }

    public void OnLanding()
    {
        animator.SetBool("Jumping", false);
        animator.SetBool("IsOnGround", false);
        Debug.Log("Has Landed");
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
         if (collision.gameObject.CompareTag("Wall"))
         {
            isOnGround = false;
           // jumpCounter = 0;

         }
        else if (collision.gameObject.CompareTag("Roof"))
        {
            isOnGround = false;
        }

        else if (collision.gameObject.CompareTag("UnlockDJ"))
        {
            hasUnlockedDJ = true;
            Destroy(collision.gameObject);
        }
        Physics2D.IgnoreLayerCollision(0,7);
    }
    //--------------------------------------------------
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.white;
        Gizmos.DrawLine(transform.position + colliderOffset, transform.position + colliderOffset + Vector3.down * groundLenght);  
        Gizmos.DrawLine(transform.position - colliderOffset, transform.position - colliderOffset + Vector3.down * groundLenght);    
    }


   


    //private void SpawnJumpRing()
    //{
    //    GameObject jumpRing = Instantiate(jumpRingPrefab) as GameObject;
    //    jumpRing.transform.position = new Vector2(this.transform.position.x, this.transform.position.y - 2.5f);
    //}

    //private void SpawnDashEffect() // Funkar inte
    //{
    //    GameObject dashEffect = Instantiate(dashEffectPrefab) as GameObject;
    //    dashEffect.transform.position = new Vector2(this.transform.position.x, this.transform.position.x - 5f);
    //}

    //IEnumerator JumpGraphic()
    //{
    //    if(!isOnGround && jumpCounter < 1)
    //    {
    //        yield return new WaitForSeconds(jRingSpawnTime);
    //        SpawnJumpRing();           
    //    }
    //}

    //IEnumerator DashGraphic()
    //{
    //    if(!isDashing)
    //    {
    //        yield return new WaitForSeconds(dESpawnTime);
    //        SpawnDashEffect();
    //    }
    //}
}

