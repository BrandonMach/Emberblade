using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LockCameraScript : MonoBehaviour
{
    // Start is called before the first frame update
    private static LockCameraScript instance;
    public Animator camAnimator;
    public GameObject player;
    
    public string desertScene;
    Scene scene;
    
    private void Start()
    {
         scene = SceneManager.GetActiveScene();
    }
    private void Update()
    {
        //if(scene.name == "DesertScene")
        //{
        //    camAnimator.SetBool("UnlockDesertCam", true);
        //}
       
    }
   

    private void OnTriggerEnter2D(Collider2D collision)
    {


        if(scene.name == "SwampScene")
        {
            if (collision.gameObject.CompareTag("Player") && this.gameObject.name == "LockCamera")
            {
                Debug.Log("Lock camera");
                camAnimator.SetBool("SwampCamera", true);
            }
            if (collision.gameObject.CompareTag("Player") && this.gameObject.name == "UnlockCamera")
            {
                Debug.Log("Unlock camera");
                camAnimator.SetBool("SwampCamera", false);
            }
        }
        if (scene.name == "DesertScene")
        {

            if (collision.gameObject.CompareTag("Player") && this.gameObject.name == "UnlockCamera"|| Checkpoint.checkpointTaken)
            {
                Debug.Log("Unlock camera");
                camAnimator.SetBool("UnlockDesertCam", false);
            }
            else if(collision.gameObject.CompareTag("Player") && this.gameObject.name == "LockCamera")
            {
                camAnimator.SetBool("UnlockDesertCam", true);
            }
            else
            {
                camAnimator.SetBool("UnlockDesertCam", true);
            }
        }   
    }

 

}
