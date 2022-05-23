using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    [SerializeField] Rigidbody2D rbody;
    [SerializeField] private float speed;
    public float lifespan = 10;
    public LayerMask layer;
    PlayerInfo player;
    bool damagePlayer = true;
    private PlayerControll playerControllScript;


    private void Start()
    {
        player = GameObject.Find("Player").GetComponent<PlayerInfo>();
        playerControllScript = GameObject.Find("Player").GetComponent<PlayerControll>();
    }
    void Update()
    {
        rbody.AddForce(transform.right * speed, ForceMode2D.Impulse);
        if (lifespan <= 0)
            Destroy(gameObject);
        else
            lifespan -= Time.deltaTime;
    }
    private void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.CompareTag("Player") && damagePlayer)
        {
            Destroy(gameObject);
            player.TakeDamage(10);
            damagePlayer = false;
            if (transform.position.x < other.transform.position.x)
            {
                playerControllScript.knockFromRight = false;
            }
            else
            {
                playerControllScript.knockFromRight = true;
            }
            playerControllScript.Knockback();
            Debug.LogError("Freeze");
            
        }
        if ( other.gameObject.CompareTag("Wall"))
        {
            Destroy(gameObject);
            
        }

    }
}