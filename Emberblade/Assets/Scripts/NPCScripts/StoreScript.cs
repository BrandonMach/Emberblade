using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StoreScript : MonoBehaviour
{
    public bool boughtHealth, boughtEnergy;
    public GameObject healthButton, energyButton;
    void Update()
    {
        if (boughtHealth)
        {
            healthButton.SetActive(false);
        }
    }

    public void buyEnergy()
    {
        if (GameObject.Find("FrogMainCharacterV1").GetComponent<Currency>().currency >= 2)
        {
            GameObject.Find("FrogMainCharacterV1").GetComponent<Currency>().currency -= 2;
            GameObject.Find("FrogMainCharacterV1").GetComponent<PlayerInfo>().maxHealth = (GameObject.Find("FrogMainCharacterV1").GetComponent<PlayerInfo>().maxHealth * 125) / 100;
            boughtHealth = true;
        }
    }

    public void buyHealth() 
    {
        //if (GameObject.Find("FrogMainCharacterV1").GetComponent<Currency>().currency > 2 && GameObject.Find("FrogMainCharacterV1").GetComponent<PlayerInfo>().currentHealth < GameObject.Find("FrogMainCharacterV1").GetComponent<PlayerInfo>().maxHealth)
        //{
        //    GameObject.Find("FrogMainCharacterV1").GetComponent<Currency>().currency -= 2;
        //    GameObject.Find("FrogMainCharacterV1").GetComponent<PlayerInfo>().currentHealth += 20;
        //    if (GameObject.Find("FrogMainCharacterV1").GetComponent<PlayerInfo>().currentHealth > GameObject.Find("FrogMainCharacterV1").GetComponent<PlayerInfo>().maxHealth)
        //    {
        //        GameObject.Find("FrogMainCharacterV1").GetComponent<PlayerInfo>().currentHealth = GameObject.Find("FrogMainCharacterV1").GetComponent<PlayerInfo>().maxHealth;
        //    }
        //    GameObject.Find("FrogMainCharacterV1").GetComponent<PlayerInfo>().healthBar.SetHealth(GameObject.Find("FrogMainCharacterV1").GetComponent<PlayerInfo>().currentHealth);
        //}

        if (GameObject.Find("FrogMainCharacterV1").GetComponent<Currency>().currency >= 2)
        {
            GameObject.Find("FrogMainCharacterV1").GetComponent<Currency>().currency -= 2;
            GameObject.Find("FrogMainCharacterV1").GetComponent<PlayerInfo>().maxHealth = (GameObject.Find("FrogMainCharacterV1").GetComponent<PlayerInfo>().maxHealth * 125) / 100;
            boughtHealth = true;
        }
    }
}
