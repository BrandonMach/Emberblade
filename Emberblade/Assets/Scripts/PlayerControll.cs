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
    private int jumpCounter = 0;
    public bool isOnGround = false;
    public GameObject jumpRingPrefab;
    public float jRingSpawnTime = 0.1f;
    public GameObject dashEffectPrefab;
    public float dESpawnTime = 0;
    private CapsuleCollider2D collider;

    Vector2 oGOffset;
    Vector2 oGSize;
    Vector3 oGpos;

    //Jump
    private float jumpTimer = 0;
    private float jumpStallTime = 0;

    //Dash
    public float dashForce;
    public float startDashTimer;
    private float currentDashTime;
    private float dashDirection;

    private bool isDashing;
    private PlayerInfo playerInfoScript;

    UnityEvent landing = new UnityEvent();
    

    public Animator animator;
    void Start()
    {
        player_Rb = GetComponent<Rigidbody2D>();
        collider = GetComponent<CapsuleCollider2D>();
        playerInfoScript = GetComponent<PlayerInfo>();
        originalJumpForce = jumpforce;


        oGSize = collider.size;
        oGOffset = collider.offset;

        landing.AddListener(OnLanding);
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        Jump();

        Debug.Log(jumpCounter);

        if (isOnGround)
        {
            landing.Invoke();
        }

        if (player_Rb.velocity.y == 7)
        {
            animator.SetBool("Falling", true);
        }
        //if (!isDashing)
        //{
        //    StartCoroutine(DashGraphic());
        //}


    }

    void Move()
    {

        float moveX = Input.GetAxisRaw("Horizontal");
        float moveBy = moveX * movementSpeed; //Variabel borde byta namn
        player_Rb.velocity = new Vector2(moveBy, player_Rb.velocity.y);
        //Flips the sprite

        Debug.Log("aaaah"+ Mathf.Abs(moveBy));
        Vector3 characterScale = transform.localScale;
        if (Input.GetAxisRaw("Horizontal") < 0)
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
        if (Input.GetAxisRaw("Horizontal") > 0)
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
        }
        else
        {
            animator.SetBool("Sit", false);
            collider.size = oGSize;
            collider.offset = oGOffset;
            movementSpeed = 25;
            jumpforce = 32;
        }

    }
    void PlayRunAnimation()
    {
        animator.SetFloat("Speed", 2);
    }

    void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && jumpCounter < 2 )
        {
            animator.SetBool("Jumping", true);
            jumpTimer = 0;        
            isOnGround = false;
            jumpCounter++;
            //v�nte tid f�r animation att hinna spela upp
            player_Rb.velocity = new Vector2(player_Rb.velocity.x, jumpforce);

            if(player_Rb.velocity.y == jumpforce)
            {
                player_Rb.gravityScale = 7;
                Debug.Log("max jump height"); 
            }
               
            
            //StartCoroutine(JumpGraphic()); //Funkar inte
            if (jumpCounter == 1)
            {
                jumpforce += 7;
            }


        }       
    }

    public void OnLanding()
    {
        animator.SetBool("Jumping", false);
        animator.SetBool("IsOnGround", false);
        Debug.Log("Has Landed");
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isOnGround = true;
            jumpCounter = 0;
            jumpforce = originalJumpForce;
            player_Rb.gravityScale = 3;

        }
        else if (collision.gameObject.CompareTag("Wall"))
        {
            isOnGround = false;
            jumpCounter = 1;

        }
        else if (collision.gameObject.CompareTag("Roof"))
        {
            isOnGround = false;
        }
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

