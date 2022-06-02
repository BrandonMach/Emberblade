using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class StoreInteraction : MonoBehaviour
{
    private bool boughtHealth, boughtEnergy;
    public GameObject healthButton, energyButton;
    private float middleOfScreenX, middleOfScreenY;
    public TextMeshProUGUI currencyText;
    Currency currency;
    PlayerInfo playerInfo;


    // Start is called before the first frame update
    void Start()
    {
        middleOfScreenX = Screen.width / 2;
        middleOfScreenY = Screen.height / 2;

        currency = GameObject.Find("Player").GetComponent<Currency>();
        playerInfo = GameObject.Find("Player").GetComponent<PlayerInfo>();
    }

    // Update is called once per frame
    void Update()
    {
        if (boughtHealth)
        {
            healthButton.SetActive(false);
            energyButton.transform.position = new Vector2 (middleOfScreenX, middleOfScreenY);
        }

        if (boughtEnergy)
        {
            energyButton.SetActive(false);
            healthButton.transform.position = new Vector2(middleOfScreenX, middleOfScreenY);
        }
        currencyText.text = currency.currency.ToString();
    }

    public void buyHealth()
    {
        if (currency.currency >= 50)
        {
            currency.currency -= 50;
            playerInfo.maxHealth = playerInfo.maxHealth += 25;
            boughtHealth = true;
        }
    }

    public void buyEnergy()
    {
        if (currency.currency >= 100)
        {
            currency.currency -= 100;
            playerInfo.maxEnergy = playerInfo.maxEnergy + 25;
            boughtEnergy = true;
        }
    }


}
