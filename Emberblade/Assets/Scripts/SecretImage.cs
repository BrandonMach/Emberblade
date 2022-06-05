using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecretImage : MonoBehaviour //Detta �r skrivet av: Philip
{
    float timer;
    public GameObject toad;
    bool activate;

    private void Update()
    {
        if (activate == true) //Om en viss tid har g�tt s� aktiveras panelen.
        {
            timer++;
            if (timer == 4000f)
            {
                toad.SetActive(true);
            }
        }
        else if (activate == false) //N�r man g�r ut s� avaktiveras den och startar om timer.
        {
            timer = 0;
            toad.SetActive(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D other) //g�r en bool till true n�r man g�r in i objektet.
    {
        if (other.tag == "Player")
        {
            activate = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other) //g�r en bool till false n�r man g�r ut ur objektet.
    {
        if (other.tag == "Player")
        {
            activate = false;
        }
    }
}
