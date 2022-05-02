using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMaster : MonoBehaviour
{
    // Start is called before the first frame update


    private static GameMaster instance;
    public Vector2 lastCheckPointPos;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(instance); //Dont destroy when switching Scenes
        }
        else
        {
            Destroy(gameObject); // Om den redan skapats skapa inte en till
        }
    }
}
