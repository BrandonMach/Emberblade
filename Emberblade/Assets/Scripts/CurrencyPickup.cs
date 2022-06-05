using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurrencyPickup : MonoBehaviour //Detta är skrivet av: Philip
{


    public int worth = 100;
     Currency currency;

    private void Start()
    {
        currency = GameObject.Find("Player").GetComponent<Currency>();
    }

    public void OnTriggerEnter2D(Collider2D collision) //Om man tar upp ett objekt som rör player så tas objektet bort och ger pengar.
    {
        if (collision.CompareTag("Player"))
        {
            Destroy(gameObject);
            currency.IncreaseCurrency(worth);
        }
    }

}
