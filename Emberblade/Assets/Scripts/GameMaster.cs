using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameMaster : MonoBehaviour
{
    // Start is called before the first frame update


    private static GameMaster instance;
    public  Vector2 lastCheckPointPos;
    public  Vector2 swampFromCavePos;
    public  Vector2 swampFromDesertPos;
    public Vector2 swampFromWinterPos;
    public Vector2 swampFromTreePos;
    static Scene scene;
    public static bool wasCave;
    public static bool wasDesert;
    public static bool wasTree;
    public static bool wasWinter;
   // public PlayerControll player;

    enum ActiveScene
    {
        Swamp,
        Cave,
        Desert
    }

    private ActiveScene activeScene;


    private void Awake()
    {
        scene = SceneManager.GetActiveScene();
        GameObject player = GameObject.Find("Player");
        

        if (scene.name == "SwampScene")
        {
            if (wasCave)
            {
               // lastCheckPointPos = swampFromCavePos;
               //wasCave = false;

            }
            if (wasDesert)
            {
                player.transform.position = swampFromDesertPos;
                //lastCheckPointPos = new Vector2(579.8f, -20.5f);
                
                Debug.LogError("was in desert scene");
            }

            else
            {
                player.transform.position = lastCheckPointPos;
                //lastCheckPointPos = new Vector2(-101.07f, 3.32f);
                Debug.LogError("I swamp scene");
            }

        }
        if (scene.name == "DesertScene")
        {
            lastCheckPointPos = new Vector2(-101.07f, 3.32f);
            Debug.LogError("I desert scene");
        }



        if (instance == null)
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
