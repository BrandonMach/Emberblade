using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class VerticalPlatform : MonoBehaviour
{
    private PlatformEffector2D effector;
    private float waitTime;
    PlayerControll player;
    private void Start()
    {
        effector = GetComponent<PlatformEffector2D>();
    }

    private void Update()
    {
        waitTime += Time.deltaTime;
        if (Input.GetKeyDown(KeyCode.S))
        {
            waitTime = 0f;
            effector.rotationalOffset = 180f;
        }
        else if (waitTime >= 0.8f)
        {
            effector.rotationalOffset = 0f;
        }
        if (Input.GetKeyDown(KeyCode.Space))
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
