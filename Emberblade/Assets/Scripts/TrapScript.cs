using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapScript : MonoBehaviour
{

    public int damageAmount = 20;
    public PlayerInfo playerDamage;

    private void Start()
    {
        playerDamage = GameObject.Find("Player").GetComponent<PlayerInfo>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player")/* && playerDamage.canTakeDamage*/)
        {
            playerDamage.TakeDamage(99999999);
            Debug.Log("Spikes");
        }
    }
}
