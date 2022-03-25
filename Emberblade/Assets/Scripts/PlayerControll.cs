using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    //Dash
    public float dashForce;
    public float startDashTimer;
    private float currentDashTime;
    private float dashDirection;

    private bool isDashing;


    void Start()
    {
        player_Rb = GetComponent<Rigidbody2D>();
        originalJumpForce = jumpforce;
        
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        Jump();

        Debug.Log(jumpCounter);
      


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
        Vector3 characterScale = transform.localScale;
        if (Input.GetAxisRaw("Horizontal") < 0)
        {
            characterScale.x = -1.45f;
        }
        if (Input.GetAxisRaw("Horizontal") > 0)
        {
            characterScale.x = 1.45f;
        }
        transform.localScale = characterScale;

        //Dash
        if (Input.GetKeyDown(KeyCode.RightShift) && moveX != 0) //Kan bara dasha om man input en direction
        {
            isDashing = true;
            currentDashTime = startDashTimer;
            player_Rb.velocity = Vector2.zero;
            dashDirection = (int)moveX;
            
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

    }

    void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && jumpCounter < 2 )
        {
            player_Rb.velocity = new Vector2(player_Rb.velocity.x, jumpforce);
            jumpCounter++;
            isOnGround = false;
            //StartCoroutine(JumpGraphic()); //Funkar inte
            if(jumpCounter == 1)
            {
                jumpforce += 7;
            }
        }    
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isOnGround = true;
            jumpCounter = 0;
            jumpforce = originalJumpForce;
        }
        else if (collision.gameObject.CompareTag("Wall") || collision.gameObject.CompareTag("Roof"))
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

