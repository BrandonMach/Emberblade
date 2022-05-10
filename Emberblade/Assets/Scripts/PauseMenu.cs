using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenuUI;
    public GameObject playerInfoUI;
    public GameObject optionMenuUI;
    public bool isPaused = false;
    public bool inOption = false;

    // Update is called once per frame
    void Update()
    {
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
        }
    }

    void Pause()
    {
        pauseMenuUI.SetActive(true);
        playerInfoUI.SetActive(false);
        isPaused = !isPaused;
    }

    public void Continue()
    {
        pauseMenuUI.SetActive(false);
        playerInfoUI.SetActive(true);
        isPaused = !isPaused;
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

    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("Quitting game..");
    }
}
