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


    // Start is called before the first frame update
    void Start()
    {
        middleOfScreenX = Screen.width / 2;
        middleOfScreenY = Screen.height / 2;
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
        //currencyText.text = GameObject.Find("Player").GetComponent<Currency>().currency.ToString();
    }

    public void buyHealth()
    {
        if (GameObject.Find("Player").GetComponent<Currency>().currency >= 50)
        {
            GameObject.Find("Player").GetComponent<Currency>().currency -= 50;
            GameObject.Find("Player").GetComponent<PlayerInfo>().maxHealth = (GameObject.Find("Player").GetComponent<PlayerInfo>().maxHealth * 125) / 100;
            boughtHealth = true;
        }
    }

    public void buyEnergy()
    {
        if (GameObject.Find("Player").GetComponent<Currency>().currency >= 100)
        {
            GameObject.Find("Player").GetComponent<Currency>().currency -= 100;
            GameObject.Find("Player").GetComponent<PlayerInfo>().maxEnergy = (GameObject.Find("Player").GetComponent<PlayerInfo>().maxEnergy * 125) / 100;
            boughtEnergy = true;
        }
    }


}
