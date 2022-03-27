using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurrencyPickup : MonoBehaviour
{
<<<<<<< Updated upstream

    public int worth = 100;
    public Currency currency;

=======
    public int worth = 100;
    public Currency currency;
>>>>>>> Stashed changes
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Destroy(gameObject);
            currency.IncreaseCurrency(worth);
        }
    }
<<<<<<< Updated upstream
=======

>>>>>>> Stashed changes
}
