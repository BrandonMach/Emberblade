using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SwitchScene : MonoBehaviour //Detta är skrivet av: Brandon + Sebastian och Philip(Uppdaterat)
{
    // Start is called before the first frame update
    Scene scene;
    void Start()
    {
        scene = SceneManager.GetActiveScene();   
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        if (collision.gameObject.CompareTag("Player") && this.gameObject.name == "ReturnToSwamp" && scene.name == "DesertScene")
        {
            GameMaster.wasCave = false;
            GameMaster.wasWinter = false;
            GameMaster.wasDesert = true; // Bool för att GM ska veta attman varit i desert
            

            Checkpoint.checkpointTaken = false; // Tar bort checkpoint spawnpos
            Debug.Log("Was Desert");
            SwitchSwamp();
        }
        if (collision.gameObject.CompareTag("Player") && this.gameObject.name == "GoToDesert" )
        {
            
            SwitchToDesert();
            GameMaster.beenInDesert = true;
        }
        if (collision.gameObject.CompareTag("Player") && this.gameObject.name == "GoToCave")
        {

            SwitchToCave();
            GameMaster.beenInCave = true;
        }
        if (collision.gameObject.CompareTag("Player") && this.gameObject.name == "ReturnToSwamp" && scene.name == "CaveScene")
        {
            GameMaster.wasDesert = false;
            GameMaster.wasWinter = false;
            GameMaster.wasCave = true; // Bool för att GM ska veta attman varit i desert
            

            Checkpoint.checkpointTaken = false; // Tar bort checkpoint spawnpos
            SwitchSwamp();
        }
        if (collision.gameObject.CompareTag("Player") && this.gameObject.name == "GoToWinter")
        {
            SwitchToWinter();
            GameMaster.beenInWinter = true;
        }
        if (collision.gameObject.CompareTag("Player") && this.gameObject.name == "ReturnToSwamp" && scene.name == "WinterScene")
        {
            GameMaster.wasDesert = false;
            GameMaster.wasCave = false;
            GameMaster.wasWinter = true; // Bool för att GM ska veta attman varit i desert
            

            Checkpoint.checkpointTaken = false; // Tar bort checkpoint spawnpos
            SwitchSwamp();
        }

    }

    public void SwitchSwamp()
    {
        SceneManager.LoadScene(1);
    }
    public void SwitchToCave()
    {
        SceneManager.LoadScene(2);
    }
    public void SwitchToDesert()
    {
        SceneManager.LoadScene(3);
    }
    public void SwitchToWinter()
    {
        SceneManager.LoadScene(4);
    }
}
