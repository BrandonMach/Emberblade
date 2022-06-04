using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecretText : MonoBehaviour //Detta är skrivet av: Philip
{
    float timer;
    public GameObject textPanel;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            textPanel.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            textPanel.SetActive(false);
        }
    }
}
