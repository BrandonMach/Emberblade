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
        if (activate == true)
        {
            timer++;
            if (timer == 4000f)
            {
                toad.SetActive(true);
            }
        }
        else if (activate == false)
        {
            timer = 0;
            toad.SetActive(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            activate = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            activate = false;
        }
    }
}
