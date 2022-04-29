using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurrencyPickup : MonoBehaviour
{


    public int worth = 100;
     Currency currency;

    private void Start()
    {
        currency = GameObject.Find("Player").GetComponent<Currency>();
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Destroy(gameObject);
            currency.IncreaseCurrency(worth);
        }
    }

}
