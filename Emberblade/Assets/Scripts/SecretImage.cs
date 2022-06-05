using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecretImage : MonoBehaviour //Detta är skrivet av: Philip
{
    float timer;
    public GameObject toad;
    bool activate;

    private void Update()
    {
        if (activate == true) //Om en viss tid har gått så aktiveras panelen.
        {
            timer++;
            if (timer == 4000f)
            {
                toad.SetActive(true);
            }
        }
        else if (activate == false) //När man går ut så avaktiveras den och startar om timer.
        {
            timer = 0;
            toad.SetActive(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D other) //gör en bool till true när man går in i objektet.
    {
        if (other.tag == "Player")
        {
            activate = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other) //gör en bool till false när man går ut ur objektet.
    {
        if (other.tag == "Player")
        {
            activate = false;
        }
    }
}
