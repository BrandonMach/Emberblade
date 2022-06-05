using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour //Detta är skrivet av: Axel
{
   public void PlayGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex +2); //När man trycker på start, eller play, så ska man tas till första scenen i spelet.
    }

    public void QuitGame()
    {
        Debug.Log("QUIT!");
        Application.Quit();                                                  //Trycker man på QUIT så ska spelet stängas ner.
    }
}
