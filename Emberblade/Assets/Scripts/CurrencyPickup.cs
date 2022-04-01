using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurrencyPickup : MonoBehaviour
{


    public int worth = 100;
    public Currency currency;
    public PlayerInfo pInfo;

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Destroy(gameObject);
            currency.IncreaseCurrency(worth);
            pInfo.TakeDamage(50);
        }
    }

}
