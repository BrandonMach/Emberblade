using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Currency : MonoBehaviour
{
    public int currency;
    public TMPro.TMP_Text currencyText;

    public void IncreaseCurrency(int amount)
    {
        currency += amount;
        currencyText.text = "" + currency;
        Debug.Log("+100");
    }

    // Update is called once per frame
    void Update()
    {
        currencyText.text = "" + currency;
    }
}
