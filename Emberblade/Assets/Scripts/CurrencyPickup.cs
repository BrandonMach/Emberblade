using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurrencyPickup : MonoBehaviour //Detta �r skrivet av: Philip
{


    public int worth = 100;
     Currency currency;

    private void Start()
    {
        currency = GameObject.Find("Player").GetComponent<Currency>();
    }

    public void OnTriggerEnter2D(Collider2D collision) //Om man tar upp ett objekt som r�r player s� tas objektet bort och ger pengar.
    {
        if (collision.CompareTag("Player"))
        {
            Destroy(gameObject);
            currency.IncreaseCurrency(worth);
        }
    }

}
