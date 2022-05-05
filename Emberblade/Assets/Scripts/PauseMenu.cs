using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenuUI;
    public GameObject playerInfoUI;
    public GameObject optionMenuUI;
    bool isPaused = false;
    bool inOption = false;

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
                    isPaused = true;
                }
                else if (isPaused)
                {
                    Continue();
                    isPaused = false;
                }
            }
        }
    }

    void Pause()
    {
        pauseMenuUI.SetActive(true);
        playerInfoUI.SetActive(false);
    }

    public void Continue()
    {
        pauseMenuUI.SetActive(false);
        playerInfoUI.SetActive(true);
    }

    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("Quitting game..");
    }
}
