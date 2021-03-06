using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerControll : MonoBehaviour //Detta ?r skrivet av: Brandon + Sebastian och Axel(platformDetection)
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

    private SFXPlaying soundEffectScript;

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

    [Header("Platform detection")]
    public LayerMask platformLayer;
    public bool isOnPlatform;

    [Header("Crouch")]
    public bool isCrouching;
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
    public bool lookingRight = true;


    [Header("Abilities")]
    public  NewAbilityTextScript newAbilityText;

    [Header("Jump")]
    [SerializeField] float jumpSpeed = 15;
    [SerializeField] float jumpDelay = 0.15f;
    private float jumpTimer = 0;
    [SerializeField] GameObject jumpRing;
    [SerializeField] bool canDoubleJump = false;
    public static bool hasUnlockedDJ; // Static g?r att boolen v?rde sparas n?r man d?r. Ska vara static i the full game
    private bool jumpRingActive;

    [Header("Dash ability")]
    [SerializeField] float dashForce;
    [SerializeField] float startDashTimer;
    private float currentDashTime;
    private float dashDirection;
    public static bool hasUnlockedDash;
    public bool isDashing { get; set; }
    private PlayerInfo playerInfoScript; //Decrease mana

   


    [Header("Parry")]
    public static bool isParrying = false;
    [SerializeField] static bool canParry = true;
    private float parryStart = 0;
    private float parryWindow = 0.5f;
    private float canParryTimer = 0;
    private float reperryTime = 3; // can parry time

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
    public float knockbackX;
    public float knockbackY;
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
        soundEffectScript = GameObject.Find("SoundManager").GetComponent<SFXPlaying>();

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
            if (knockFromRight)                                                 //Om spelaren blir attackerad fr?n h?ger
            { //knock to left
                player_Rb.velocity = new Vector2(-knockbackX, knockbackY);
            }
            if (!knockFromRight)
            {
                player_Rb.velocity = new Vector2(knockbackX, knockbackY);
            }
            knockbackCount -= Time.deltaTime;
        }

        Falling();
        if (!hasUnlockedDJ)                 // Om spelaren inte har l?st upp double jump
        {
            canDoubleJump = false;
            jumpTimer = 0;
        }
        JumpInput();
        isOnGround = Physics2D.Raycast(transform.position + colliderOffset, Vector2.down, groundLenght, groundLayer) || Physics2D.Raycast(transform.position - colliderOffset, Vector2.down, groundLenght, groundLayer);        //F?r att detectera om splaren st?r p? marken
        isOnPlatform = Physics2D.Raycast(transform.position + colliderOffset, Vector2.down, groundLenght, platformLayer) || Physics2D.Raycast(transform.position - colliderOffset, Vector2.down, groundLenght, platformLayer);   //F?r att detectera om splaren st?r p? platform
        if (Input.GetKeyDown(KeyCode.Space) && canDoubleJump && !isOnGround)
        {
            canDoubleJump = false;
            SpawnJumpRing();
            
        }
        Parry();

        hasToCrouch = Physics2D.Raycast(transform.position + headOffset, Vector2.up, headFloat, groundLayer)|| Physics2D.Raycast(transform.position + headOffset, Vector2.up, headFloat,roofLayer);         //En raycast f?r att kolla om spalaren m?ste forts?tta ducka
        Crouch();
        jumpTimer += Time.deltaTime;
        if (jumpTimer >= jumpDelay)
        {
            jumpRingActive = false;
            jumpRing.SetActive(false);
        }

        if (!canParry)                      //F?r att att man inte ska kunna parry flera g?nger i rad
        {
            
            canParryTimer += Time.deltaTime;
            if (canParryTimer  >= reperryTime)
            {
                canParry = true;
                Debug.LogError("you can perry now");
                canParryTimer = 0;
            }
            
        }

    }

    void Parry()
    {
        if (Input.GetKeyDown(KeyCode.H) && canParry)
        {
            parryStart = 0;
            isParrying = true;
        }
        if (isParrying)
        {
            canParry = false;
            Debug.Log("Parrying");
            animator.SetBool("Parry", true);
            parryStart += Time.deltaTime;
            if (parryStart >= parryWindow)
            {
                isParrying = false;
                Debug.LogWarning("test");
                //parryStart = 0;
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
        if (isOnPlatform)
        {
            canDoubleJump = true;
        }
        else
        {
            player_Rb.gravityScale = gravity;
            player_Rb.drag = liniearDrag * 0.15f;
            if (player_Rb.velocity.y < 0)
            {
                player_Rb.gravityScale = gravity * fallMultiplier;
            }
            else if (player_Rb.velocity.y > 0 && !Input.GetButton("Jump")) // Om spelaren inte h?ller ner hoppknappen joppar man kortare
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
            lookingRight = false;
            if (isOnGround || isOnPlatform)
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
            lookingRight = true;
            if (isOnGround || isOnPlatform)
            {
                PlayRunAnimation();
            }
           
        }
      
        transform.localScale = characterScale;

        //Dash
        if (hasUnlockedDash)            //Om spelaren har l?st upp dash
        {
            if (Input.GetKeyDown(KeyCode.L) && moveX != 0 && PlayerInfo.currentEnergy >= 20) //Kan bara dasha om man input en direction
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
            isTouchingFront = Physics2D.OverlapCircle(frontCheck.position, 5, wallLayer);                  //Kollar om spelaren ?r vid en v?gg 
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
                    Invoke("SetWallJumpingFalse", wallJumpTime); // B?ttre timer ?n att loopa aka s?tter p? Metoden SetWallJumpingFalse efter wallJumpTime
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
                    player_Rb.velocity = new Vector2(xWallForce * -moveX, yWallForce);  //Reverse input f?r att hoppa motsatt fr?n v?ggen
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
        if (Input.GetKey(KeyCode.C) || hasToCrouch && isOnGround || hasToCrouch && isOnPlatform)            
        {
            animator.SetBool("Sit", true);
            capsuleCollider.size = new Vector2(4.577552f, 4.577552f);
            capsuleCollider.offset = new Vector2(-0.01422455f, -2.3f);
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
        if (Input.GetKeyDown(KeyCode.Space) && isOnGround || Input.GetKeyDown(KeyCode.Space)  && canDoubleJump || Input.GetKeyDown(KeyCode.Space) && isOnPlatform)
        {
            soundEffectScript.PlayJump();
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

    public void Knockback(float kbX, float kbY)
    {
        knockbackX = kbX;
        knockbackY = kbY;
        Debug.Log("Knockback");
        knockbackCount = knockbackLength;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Physics2D.IgnoreLayerCollision(11, 7);                     //Player Layer Ignore Grapple layer 
        if (collision.gameObject.CompareTag("Wall"))
         {
            isOnGround = false;
            isOnPlatform = false;
         }
        if (collision.gameObject.CompareTag("Roof"))
        {
            isOnGround = false;
            isOnPlatform = false;
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

    public void PlayNewAbilityCutscene()            //spelar en cutscene n?r spelaren f?r en ny f?rm?ga
    {     
        camAnimator.SetBool("NewAbility", true);
        this.enabled = false;
        Invoke("StopCutscene", 1);
        newAbilityText.startText = true;
    }
    public void OpenChestCutScene()                 //spelar en cutscene n?r spelaren ?pnar en ny f?rm?ga
    {
        camAnimator.SetBool("OpenChest", true);
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
        camAnimator.SetBool("OpenChest", false);
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

