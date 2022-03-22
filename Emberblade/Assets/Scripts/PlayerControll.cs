using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControll : MonoBehaviour
{
    // Start is called before the first frame update
    public float movementSpeed = 1;
    private Rigidbody2D player_Rb;

    public float jumpforce = 1;
    private int jumpCounter = 0;
    public bool isOnGround = false;
    public GameObject jumpRingPrefab;
    public float jRingSpawnTime = 0.1f;


    void Start()
    {
        player_Rb = GetComponent<Rigidbody2D>();
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
        float moveBy = moveX * movementSpeed;
        player_Rb.velocity = new Vector2(moveBy, player_Rb.velocity.y);
    }

    void Jump()
    {
        
        if (Input.GetKeyDown(KeyCode.Space) && jumpCounter < 2 )
        {
           
            player_Rb.velocity = new Vector2(player_Rb.velocity.x, jumpforce);
            jumpCounter++;
            isOnGround = false;
            StartCoroutine(JumpGraphic());

        }
      
      
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        isOnGround = true;
        jumpCounter = 0;
    }

    private void SpawnJumpRing()
    {
        GameObject a = Instantiate(jumpRingPrefab) as GameObject;
        a.transform.position = new Vector2(this.transform.position.x, this.transform.position.y - 2.5f);
    }

    IEnumerator JumpGraphic()
    {
        if(!isOnGround && jumpCounter == 1)
        {
            yield return new WaitForSeconds(jRingSpawnTime);

            SpawnJumpRing(); 
            
        }
        
    }
}
