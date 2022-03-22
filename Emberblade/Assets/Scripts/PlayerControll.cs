using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControll : MonoBehaviour
{
    // Start is called before the first frame update
    public float movementSpeed = 1;
    private Rigidbody2D player_Rb;

    void Start()
    {
        player_Rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    void Move()
    {
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveBy = moveX * movementSpeed;
        player_Rb.velocity = new Vector2(moveBy, player_Rb.velocity.y);
    }
}
