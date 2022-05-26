using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class VerticalPlatform : MonoBehaviour
{
    private PlatformEffector2D effector;
    private float waitTime;
    public PlayerControll player;
    private void Start()
    {
        effector = GetComponent<PlatformEffector2D>();
    }

    private void Update()
    {
        waitTime += Time.deltaTime;
        if (Input.GetKey(KeyCode.S))
        {
            effector.rotationalOffset = 180f;
            player.player_Rb.gravityScale = 10;
            waitTime = 0f;
        }
        else if (waitTime >= 0.3f)
        {
            effector.rotationalOffset = 0f;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("collision");
            player.isOnGround = true;
        }
    }

}
