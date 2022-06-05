using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour //Detta är skrivet av: Philip + Axel
{
    public GameObject pauseMenuUI;
    public GameObject playerInfoUI;
    public GameObject optionMenuUI;
    public TextMeshProUGUI HP, Mana, AD;
    public GameObject tabPanel, Dash, Double, GP;
    public GameObject winter, desert, cave;
    public bool isPaused = false;
    public bool inOption = false;
    public bool inTabMenu = false;

    // Update is called once per frame

    void Update()
    {

        HP.text = "HP: " + PlayerInfo.currentHealth + "/" + PlayerInfo.maxHealth; //Visar liv
        Mana.text = "Mana: " + PlayerInfo.currentEnergy + "/" + PlayerInfo.maxEnergy; //Visar mana
        AD.text = "Attack Damage: " + CombatScript.playerDamage; //Visar attack damage

        if (inOption == false)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                if (!isPaused)
                {
                    Pause();
                }
                else if (isPaused)
                {
                    Continue();
                }
            }
            if (Input.GetKeyDown(KeyCode.Tab))
            {
                if (!inTabMenu)
                {
                    Tab();
                }
                else if(inTabMenu)
                {
                    ContinueTab();
                }
            }
        }

        if (PlayerControll.hasUnlockedDash)
        {
            Dash.SetActive(true);
        }
        if (PlayerControll.hasUnlockedDJ)
        {
            Double.SetActive(true);
        }



        if (GameMaster.beenInDesert)
        {
            desert.SetActive(true);
        }
        if (GameMaster.beenInCave)
        {
            cave.SetActive(true);
        }
        if (GameMaster.beenInWinter)
        {
            winter.SetActive(true);
        }


    }

    void Pause() //Om man pausar så blir panel true och and false
    {
        pauseMenuUI.SetActive(true);
        playerInfoUI.SetActive(false);
        isPaused = !isPaused;
        tabPanel.SetActive(false);
        inTabMenu = false;
    }

    public void Continue() //Om man pausar så blir panel true och and false
    {
        pauseMenuUI.SetActive(false);
        tabPanel.SetActive(false);
        playerInfoUI.SetActive(true);
        isPaused = !isPaused;
    }
    public void ContinueTab() //Om man pausar så blir panel true och and false
    {
        pauseMenuUI.SetActive(false);
        tabPanel.SetActive(false);
        playerInfoUI.SetActive(true);
        inTabMenu = !inTabMenu;

    }

    public void Option() //Om man pausar så blir panel true och and false
    {
        pauseMenuUI.SetActive(false);
        playerInfoUI.SetActive(false);
        optionMenuUI.SetActive(true);
        inOption = !inOption;
    }

    public void BackOption() //Om man pausar så blir panel true och and false
    {
        pauseMenuUI.SetActive(true);
        playerInfoUI.SetActive(false);
        optionMenuUI.SetActive(false);
        inOption = !inOption;
    }

    public void Tab() //Om man pausar så blir panel true och and false
    {
        pauseMenuUI.SetActive(false);
        tabPanel.SetActive(true);
        playerInfoUI.SetActive(false);
        inTabMenu = !inTabMenu;
        isPaused = false;
    }

    public void QuitGame() //Avslutar spelet
    {
        SceneManager.LoadScene(0);
        Debug.Log("Quitting game..");
    }
}
