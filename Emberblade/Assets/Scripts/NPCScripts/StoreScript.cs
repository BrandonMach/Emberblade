using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class StoreScript : MonoBehaviour //Detta är skrivet av: Axel, en gammal version som inte används, se "StoreInteraction" scriptet för den nya versionen.
{
    public bool boughtHealth, boughtEnergy;
    public GameObject healthButton, energyButton;
    private float middleOfScreenX, middleOfScreenY;
    public TextMeshProUGUI currencyText;
   // Currency playerCurrency;

    private void Start()
    {
        middleOfScreenX = Screen.width / 2;
        middleOfScreenY = Screen.height / 2;   
    }
    void Update()
    {
        if (boughtHealth)
        {
            healthButton.SetActive(false);
            energyButton.transform.position = new Vector2(middleOfScreenX,middleOfScreenY);
        }
        if (boughtEnergy)
        {
            energyButton.SetActive(false);
            healthButton.transform.position = new Vector2(middleOfScreenX,middleOfScreenY); 
        }


    }

    public void buyEnergy()
    {
        if (Currency.currency >= 100)
        {
            Currency.currency -= 100;
            PlayerInfo.maxEnergy = (PlayerInfo.maxEnergy * 125) / 100;
            boughtEnergy = true;
        }

    }

    public void buyHealth() 
    {

        if (Currency.currency >= 2)// Denan kommer aldrig att gå igenom eftersom det finns inge FrogMainCharacterV1
        {
            Currency.currency -= 2;
            PlayerInfo.maxHealth = (PlayerInfo.maxHealth * 125) / 100;
            boughtHealth = true;
        }
    }
}
