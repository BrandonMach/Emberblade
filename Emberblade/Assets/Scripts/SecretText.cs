using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecretText : MonoBehaviour //Detta �r skrivet av: Philip
{
    float timer;
    public GameObject textPanel;

    private void OnTriggerEnter2D(Collider2D other) //aktiverar en panel n�r man g�r in i den
    {
        if (other.tag == "Player")
        {
            textPanel.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D other) //avaktiverar en panel n�r man g�r in i den
    {
        if (other.tag == "Player")
        {
            textPanel.SetActive(false);
        }
    }
}
