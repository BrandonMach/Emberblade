using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerControll : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] float movementSpeed = 1;
    public Rigidbody2D player_Rb;

    [SerializeField] float jumpforce = 1;
    private float originalJumpForce;
    
    [SerializeField] float jRingSpawnTime = 0.1f;
    [SerializeField] GameObject dashEffectPrefab;
    [SerializeField] float dESpawnTime = 0;
    private CapsuleCollider2D capsuleCollider;
    private BoxCollider2D boxCollider;

   

    Vector2 standingBoxOffset;
    Vector2 stadningBoxSize;





    [SerializeField] Animator animator;
    [SerializeField] Animator camAnimator;

    //---------------------------------
    [Header ("Ground detection")]
    [SerializeField] LayerMask groundLayer;
    public bool isOnGround;
    [SerializeField] float groundLenght;
    [SerializeField] Vector3 colliderOffset;
    UnityEvent landing = new UnityEvent();

    [Header("Crouch")]
    [SerializeField] bool isCrouching;
    [SerializeField] bool hasToCrouch;
    [SerializeField] LayerMask roofLayer;
    [SerializeField] float headFloat;
    [SerializeField] Vector3 headOffset;
    Vector2 oGOffset;
    Vector2 oGSize;
    Vector3 oGpos;



    [Header("Physics")]
    [SerializeField] float gravity = 1;
    [SerializeField] float fallMultiplier = 5f;
    [SerializeField] float liniearDrag = 4f;

    [Header("Abilities")]
    public  NewAbilityTextScript newAbilityText;

    [Header("Jump")]
    [SerializeField] float jumpSpeed = 15;
    [SerializeField] float jumpDelay = 0.15f;
    private float jumpTimer = 0;
    [SerializeField] GameObject jumpRing;
    [SerializeField] bool canDoubleJump = false;
    [SerializeField] static bool hasUnlockedDJ; // Static gör att boolen värde sparas när man dör. Ska vara static i the full game
    private bool jumpRingActive;

    [Header("Dash ability")]
    [SerializeField] float dashForce;
    [SerializeField] float startDashTimer;
    private float currentDashTime;
    private float dashDirection;
    [SerializeField] bool hasUnlockedDash;
    public bool isDashing { get; set; }
    private PlayerInfo playerInfoScript; //Decrease mana

   


    [Header("Parry")]
    public bool isParrying = false;
    private float parryStart = 0;
    private float parryWindow = 0.5f;

    [Header("Hook")]
    [SerializeField] GrappleHookScript ghScript;
    [SerializeField] LayerMask grapplelayer;

    [Header("WallClimb")]
    [SerializeField] LayerMask wallLayer;
    bool isTouchingFront;
    [SerializeField] Transform frontCheck;
    bool wallCling;
    [SerializeField] float wallClimbSpeed;
    [SerializeField] bool CanWallClimb;

    [Header("Wall Jump")]
    bool wallJumping;
    [SerializeField] float xWallForce;
    [SerializeField] float yWallForce;
    [SerializeField] float wallJumpTime;

    [Header("Knockback")]
    public float knockback;
    public float knockbackLength;
    public float knockbackCount;
    public bool knockFromRight;
    

    void Start()
    {
        player_Rb = GetComponent<Rigidbody2D>();
        capsuleCollider = GetComponent<CapsuleCollider2D>();
        boxCollider = GetComponent<BoxCollider2D>();
        playerInfoScript = GetComponent<PlayerInfo>();
        newAbilityText = GameObject.Find("NewAbilityController").GetComponent<NewAbilityTextScript>();
        originalJumpForce = jumpforce;


        oGSize = capsuleCollider.size;
        oGOffset = capsuleCollider.offset;

        standingBoxOffset = boxCollider.offset;
        stadningBoxSize = boxCollider.size;

        landing.AddListener(OnLanding);
    }

    // Update is called once per frame
    void Update()
    {
        //Knockback
        if (knockbackCount <= 0)
        {
            Move();
        }
        else
        {
            if (knockFromRight)
            { //knock to left
                player_Rb.velocity = new Vector2(-knockback, knockback);
            }
            if (!knockFromRight) {
                player_Rb.velocity = new Vector2(knockback, knockback);
            }
            knockbackCount -= Time.deltaTime;
        }
        
        Falling();
        if (!hasUnlockedDJ)
        {
            canDoubleJump = false;
            jumpTimer = 0;
        }
        JumpInput();
        isOnGround = Physics2D.Raycast(transform.position + colliderOffset, Vector2.down, groundLenght, groundLayer) || Physics2D.Raycast(transform.position - colliderOffset, Vector2.down, groundLenght, groundLayer);
        if (Input.GetKeyDown(KeyCode.Space) && canDoubleJump && !isOnGround)
        {
            canDoubleJump = false;
            SpawnJumpRing();
            
        }
        Parry();

        hasToCrouch = Physics2D.Raycast(transform.position + headOffset, Vector2.up, headFloat, groundLayer)|| Physics2D.Raycast(transform.position + headOffset, Vector2.up, headFloat,roofLayer);
        Crouch();
        jumpTimer += Time.deltaTime;
        if (jumpTimer >= jumpDelay)
        {
            jumpRingActive = false;
            jumpRing.SetActive(false);
        }

    }

    void Parry()
    {
        if (Input.GetKeyDown(KeyCode.H))
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
        if (hasUnlockedDash)
        {
            if (Input.GetKeyDown(KeyCode.L) && moveX != 0 && playerInfoScript.currentEnergy >= 20) //Kan bara dasha om man input en direction
            {
                animator.SetTrigger("Dash");
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
                
                if (currentDashTime <= 0)
                {
                    
                    isDashing = false;
                }

            }
        }
       
      

        //WallClimb
        if (CanWallClimb) 
        {
            isTouchingFront = Physics2D.OverlapCircle(frontCheck.position, 5, wallLayer);
            if (isTouchingFront && !isOnGround && moveX != 0)
            {
                wallCling = true;
            }
            else
            {
                wallCling = false;
            }
            if (wallCling)
            {
                //Wall jump
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    wallJumping = true;
                    Invoke("SetWallJumpingFalse", wallJumpTime); // Bättre timer än att loopa aka sätter på Metoden SetWallJumpingFalse efter wallJumpTime
                }
                else if (Input.GetKey(KeyCode.W))
                {
                    player_Rb.velocity = new Vector2(player_Rb.velocity.x, 10);
                    PlayClimbAnimation();
                }
                else
                {
                    player_Rb.velocity = new Vector2(player_Rb.velocity.x, Mathf.Clamp(player_Rb.velocity.y, -wallClimbSpeed, float.MaxValue));
                    PlayClimbAnimation();
                }

                if (wallJumping)
                {
                    player_Rb.velocity = new Vector2(xWallForce * -moveX, yWallForce);  //Reverse input för att hoppa motsatt från väggen
                }
            }
            else
            {
                animator.SetFloat("Climb", 0);
            }
        }

    }

    void Crouch()
    {
        //Crouch
        if (Input.GetKey(KeyCode.C) || hasToCrouch && isOnGround)
        {
            animator.SetBool("Sit", true);
            capsuleCollider.size = new Vector2(4.577552f, 4.577552f);
            capsuleCollider.offset = new Vector2(-0.01422455f, -1f);
            movementSpeed = 13;
            jumpforce = 20;
            boxCollider.offset = new Vector2(-0.05918601f, -2.19f);
            boxCollider.size = new Vector2(3.4216f, 5);
            isCrouching = true;
        }
        else
        {
            animator.SetBool("Sit", false);
            isCrouching = false;
            capsuleCollider.size = oGSize;
            capsuleCollider.offset = oGOffset;
            movementSpeed = 25;
            jumpforce = 40;
            boxCollider.offset = standingBoxOffset;
            boxCollider.size = stadningBoxSize;
        }
    }
    void SetWallJumpingFalse()
    {
        wallJumping = false;
    }
    void PlayRunAnimation()
    {
        animator.SetFloat("Speed", 2);
    }
    void PlayClimbAnimation()
    {
        animator.SetFloat("Climb", 2);     
    }

    void JumpInput()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isOnGround || Input.GetKeyDown(KeyCode.Space)  && canDoubleJump)
        {
            animator.SetBool("Jumping", true);
                Jumping();
        }       
    }
    void Jumping()
    {

        if (isCrouching)
        {
            player_Rb.velocity = new Vector2(player_Rb.velocity.x, 0);
            player_Rb.AddForce(Vector2.up * jumpSpeed*0.75f, ForceMode2D.Impulse);
            jumpTimer = 0;
        }
        else
        {
            player_Rb.velocity = new Vector2(player_Rb.velocity.x, 0);
            player_Rb.AddForce(Vector2.up * jumpSpeed, ForceMode2D.Impulse);
            jumpTimer = 0;
        }
       
    }
    void SpawnJumpRing()
    {
        jumpRing.SetActive(true);
        jumpRingActive = true;
       
    }

    public void OnLanding()
    {
        animator.SetBool("Jumping", false);
        animator.SetBool("IsOnGround", false);
    }

    public void Knockback()
    {
        Debug.Log("Knockback");
        knockbackCount = knockbackLength;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Physics2D.IgnoreLayerCollision(11, 7); // Player Layer Ignore Grapple layer 
        if (collision.gameObject.CompareTag("Wall"))
         {
            isOnGround = false;
         }
        if (collision.gameObject.CompareTag("Roof"))
        {
            isOnGround = false;
        }

        else if (collision.gameObject.CompareTag("UnlockDJ"))
        {
            hasUnlockedDJ = true;
            Destroy(collision.gameObject);
            PlayNewAbilityCutscene();
            newAbilityText.index = 0;

        }
        else if (collision.gameObject.CompareTag("UnlockDash"))
        {
            hasUnlockedDash = true;
            Destroy(collision.gameObject);
            PlayNewAbilityCutscene();
            newAbilityText.index = 1;
        }         
    }

    void PlayNewAbilityCutscene()
    {     
        camAnimator.SetBool("NewAbility", true);
        this.enabled = false;
        Invoke("StopCutscene", 1);
        newAbilityText.startText = true;
    }


    //--------------------------------------------------

    void StopCutscene()
    {
        Debug.Log("YO");
        newAbilityText.startText = false;
        newAbilityText.ClosePopUP();
        this.enabled = true;
        camAnimator.SetBool("NewAbility", false);
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.white;
        Gizmos.DrawLine(transform.position + colliderOffset, transform.position + colliderOffset + Vector3.down * groundLenght);  
        Gizmos.DrawLine(transform.position - colliderOffset, transform.position - colliderOffset + Vector3.down * groundLenght);

        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position + headOffset, transform.position + headOffset + Vector3.up * headFloat);
    }


}

