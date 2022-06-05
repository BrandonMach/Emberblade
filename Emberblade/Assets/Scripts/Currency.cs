using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Currency : MonoBehaviour //Detta �r skrivet av: Philip
{
    public static int currency;
    public TMPro.TMP_Text currencyText;

    public void IncreaseCurrency(int amount) //�kar din valuta.
    {
        currency += amount;
        currencyText.text = "" + currency;
        Debug.Log("+100");
    }

    public void LoseCurrency(int amount) //G�r s� du f�rlorar valuta.
    {
        currency -= amount;
        currencyText.text = "" + currency;
    }

    // Update is called once per frame
    void Update() //Uppdaterar texten och s� om du d�r s� f�rlorar du all ditt liv.
    {
        currencyText.text = "" + currency;
        if (PlayerInfo.currentHealth <= 0)
        {
            LoseCurrency(currency);
        }
    }
}
