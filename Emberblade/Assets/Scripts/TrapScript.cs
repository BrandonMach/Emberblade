using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapScript : MonoBehaviour
{

    public int damageAmount = 20;
    public PlayerInfo playerDamage;

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerDamage.TakeDamage(99999);
        }
    }
}
