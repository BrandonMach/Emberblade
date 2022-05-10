using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockCameraScript : MonoBehaviour
{
    // Start is called before the first frame update

    public Animator camAnimator;
    public GameObject player;

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.gameObject.CompareTag("Player")&& this.gameObject.name== "LockCamera")
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
}
