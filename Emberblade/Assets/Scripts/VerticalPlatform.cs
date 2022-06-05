using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class VerticalPlatform : MonoBehaviour //Detta är skrivet av: Philip + Sebastian
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
        waitTime += Time.deltaTime; //Adderar tiden.
        if (Input.GetKey(KeyCode.S)) //Om man trycker S så ska platformenshitbox rotera och man kan gå ner.
        {
            effector.rotationalOffset = 180f;
            player.player_Rb.gravityScale = 10;
            waitTime = 0f;
        }
        else if (waitTime >= 0.3f) //Efter en viss tid så går den tillbaka till normala.
        {
            effector.rotationalOffset = 0f;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player")) //Om man kolliderar så blir bool true.
        {
            Debug.Log("collision");
            player.isOnGround = true;
        }
    }

}
