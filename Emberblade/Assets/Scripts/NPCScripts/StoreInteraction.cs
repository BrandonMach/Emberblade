using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class StoreInteraction : MonoBehaviour //Detta är skrivet av: Axel
{
    private bool boughtHealth, boughtEnergy;
    public GameObject healthButton, energyButton;
    private float middleOfScreenX, middleOfScreenY;
    public TextMeshProUGUI currencyText;
    //Currency currency;
    PlayerInfo playerInfo;


    // Start is called before the first frame update
    void Start()
    {
        middleOfScreenX = Screen.width / 2;
        middleOfScreenY = Screen.height / 2;

        //currency = GameObject.Find("Player").GetComponent<Currency>();
        playerInfo = GameObject.Find("Player").GetComponent<PlayerInfo>();
    }

    // Update is called once per frame
    void Update()
    {
        if (boughtHealth)       //tar bort knappen från skärmen efter att man har köpt föremålet. Flyttar även andra knappen till mitten av skärmen.
        {
            healthButton.SetActive(false);
            energyButton.transform.position = new Vector2 (middleOfScreenX, middleOfScreenY);
        }

        if (boughtEnergy)       //tar bort knappen från skärmen efter att man har köpt föremålet. Flyttar även andra knappen till mitten av skärmen.
        {
            energyButton.SetActive(false);
            healthButton.transform.position = new Vector2(middleOfScreenX, middleOfScreenY);
        }

        currencyText.text = Currency.currency.ToString();
    }

    public void buyHealth()     //ökar spelarens hälsa och tar bort pengar från spelaren.
    {
        if (Currency.currency >= 50)
        {
            Currency.currency -= 50;
            PlayerInfo.maxHealth = PlayerInfo.maxHealth += 25;
            boughtHealth = true;
        }
    }

    public void buyEnergy()     //ökar spelarens mana/energi och tar bort pengar från spelaren.
    {
        if (Currency.currency >= 100)
        {
            Currency.currency -= 100;
            PlayerInfo.maxEnergy = PlayerInfo.maxEnergy + 25;
            boughtEnergy = true;
        }
    }


}
