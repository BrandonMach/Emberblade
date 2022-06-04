using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PauseMenu : MonoBehaviour
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

        HP.text = "HP: " + PlayerInfo.currentHealth + "/" + PlayerInfo.maxHealth;
        Mana.text = "Mana: " + PlayerInfo.currentEnergy + "/" + PlayerInfo.maxEnergy;
        AD.text = "Attack Damage: " + CombatScript.playerDamage;

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

    void Pause()
    {
        pauseMenuUI.SetActive(true);
        playerInfoUI.SetActive(false);
        isPaused = !isPaused;
        tabPanel.SetActive(false);
        inTabMenu = false;
    }

    public void Continue()
    {
        pauseMenuUI.SetActive(false);
        tabPanel.SetActive(false);
        playerInfoUI.SetActive(true);
        isPaused = !isPaused;
    }
    public void ContinueTab()
    {
        pauseMenuUI.SetActive(false);
        tabPanel.SetActive(false);
        playerInfoUI.SetActive(true);
        inTabMenu = !inTabMenu;

    }

    public void Option()
    {
        pauseMenuUI.SetActive(false);
        playerInfoUI.SetActive(false);
        optionMenuUI.SetActive(true);
        inOption = !inOption;
    }

    public void BackOption()
    {
        pauseMenuUI.SetActive(true);
        playerInfoUI.SetActive(false);
        optionMenuUI.SetActive(false);
        inOption = !inOption;
    }

    public void Tab()
    {
        pauseMenuUI.SetActive(false);
        tabPanel.SetActive(true);
        playerInfoUI.SetActive(false);
        inTabMenu = !inTabMenu;
        isPaused = false;
    }

    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("Quitting game..");
    }
}
