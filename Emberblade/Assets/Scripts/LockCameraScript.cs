using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LockCameraScript : MonoBehaviour
{
    // Start is called before the first frame update
    private static LockCameraScript instance;
    [SerializeField] Animator camAnimator;
    [SerializeField] GameObject player;

    Scene scene;
    
    private void Start()
    {
         scene = SceneManager.GetActiveScene();
    }
    private void Update()
    {
       
    }
   

    private void OnTriggerEnter2D(Collider2D collision)
    {


        if(scene.name == "SwampScene")
        {
            if (collision.gameObject.CompareTag("Player") && this.gameObject.name == "LockCamera")
            {
                
                camAnimator.SetBool("SwampCamera", true);
            }
            if (collision.gameObject.CompareTag("Player") && this.gameObject.name == "UnlockCamera")
            {
                Debug.Log("Unlock camera");
                camAnimator.SetBool("SwampCamera", false);
            }
            if (collision.gameObject.CompareTag("Player") && this.gameObject.name == "LockWinterCam")
            {
                Debug.Log("Lock camera");
                camAnimator.SetBool("WinterSwampLock", true);
            }
            if (collision.gameObject.CompareTag("Player") && this.gameObject.name == "UnlockWinterCam")
            {
                Debug.Log("Unlock camera");
                camAnimator.SetBool("WinterSwampLock", false);
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
