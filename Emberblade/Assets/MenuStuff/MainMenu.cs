using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour //Detta �r skrivet av: Axel
{
   public void PlayGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex +2); //N�r man trycker p� start, eller play, s� ska man tas till f�rsta scenen i spelet.
    }

    public void QuitGame()
    {
        Debug.Log("QUIT!");
        Application.Quit();                                                  //Trycker man p� QUIT s� ska spelet st�ngas ner.
    }
}
