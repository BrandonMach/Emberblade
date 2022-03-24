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
            characterScale.x = -2;
        }
        if (Input.GetAxisRaw("Horizontal") > 0)
        {
            characterScale.x = 2;
        }
        transform.localScale = characterScale;

        if (Input.GetKeyDown(KeyCode.RightShift) && moveX != 0)
        {
            isDashing = true;
            currentDashTime = startDashTimer;
            player_Rb.velocity = Vector2.zero;
            dashDirection = (int)moveX;
        }
        if (isDashing)
        {
            player_Rb.velocity = transform.right * dashDirection * dashForce;
            currentDashTime  -= Time.deltaTime;

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
            StartCoroutine(JumpGraphic());
            if(jumpCounter == 1)
            {
                jumpforce += 7;
            }
        }
      
      
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        isOnGround = true;
        jumpCounter = 0;
        jumpforce = originalJumpForce;
    }


    private void SpawnJumpRing()
    {
        GameObject a = Instantiate(jumpRingPrefab) as GameObject;
        a.transform.position = new Vector2(this.transform.position.x, this.transform.position.y - 2.5f);
    }

    IEnumerator JumpGraphic()
    {
        if(!isOnGround && jumpCounter < 1)
        {
            yield return new WaitForSeconds(jRingSpawnTime);

            SpawnJumpRing(); 
            
        }
        
    }
}

