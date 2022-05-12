using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameMaster : MonoBehaviour
{
    // Start is called before the first frame update


    private static GameMaster instance;
    public Vector2 lastCheckPointPos;
    public Vector2 desertSpawn;
    Scene scene;
    public static bool wasDesert;
    public PlayerControll player;

    enum ActiveScene
    {
        Swamp,
        Cave,
        Desert
    }

    private ActiveScene activeScene;
    private void Start()
    {
        scene = SceneManager.GetActiveScene();
    }
    private void Update()
    {
        Debug.Log("Yo" + wasDesert);
    }

    private void Awake()
    {
        //if (wasDesert)
        //{
        //    if (gameObject.CompareTag("Player"))
        //    {
        //        player.transform.position = desertSpawn;
        //    }
        //}
       

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
