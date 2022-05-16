using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointActivate : MonoBehaviour
{

    public bool triggering;
    public Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        if (triggering)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                animator.SetTrigger("Entry");
            }
        }
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            triggering = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            triggering = false;
        }
    }
}
