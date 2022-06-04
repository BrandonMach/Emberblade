using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Currency : MonoBehaviour
{
    public static int currency;
    public TMPro.TMP_Text currencyText;

    public void IncreaseCurrency(int amount)
    {
        currency += amount;
        currencyText.text = "" + currency;
        Debug.Log("+100");
    }

    public void LoseCurrency(int amount)
    {
        currency -= amount;
        currencyText.text = "" + currency;
    }

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        currencyText.text = "" + currency;
        if (PlayerInfo.currentHealth <= 0)
        {
            LoseCurrency(currency);
        }
    }
}
