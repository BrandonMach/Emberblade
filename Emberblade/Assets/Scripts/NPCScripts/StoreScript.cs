using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StoreScript : MonoBehaviour
{
    void Update()
    {
    }

    public void buyFlask()
    {

    }

    public void buyFood() 
    {
        if (GameObject.Find("FrogMainCharacterV1").GetComponent<Currency>().currency > 2 && GameObject.Find("FrogMainCharacterV1").GetComponent<PlayerInfo>().currentHealth < GameObject.Find("FrogMainCharacterV1").GetComponent<PlayerInfo>().maxHealth)
        {
            GameObject.Find("FrogMainCharacterV1").GetComponent<Currency>().currency -= 2;
            GameObject.Find("FrogMainCharacterV1").GetComponent<PlayerInfo>().currentHealth += 20;
            if (GameObject.Find("FrogMainCharacterV1").GetComponent<PlayerInfo>().currentHealth > GameObject.Find("FrogMainCharacterV1").GetComponent<PlayerInfo>().maxHealth)
            {
                GameObject.Find("FrogMainCharacterV1").GetComponent<PlayerInfo>().currentHealth = GameObject.Find("FrogMainCharacterV1").GetComponent<PlayerInfo>().maxHealth;
            }
            GameObject.Find("FrogMainCharacterV1").GetComponent<PlayerInfo>().healthBar.SetHealth(GameObject.Find("FrogMainCharacterV1").GetComponent<PlayerInfo>().currentHealth);
        }
    }
}
