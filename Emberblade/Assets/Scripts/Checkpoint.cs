using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    private GameMaster gm;
    public Animator animator;
    public static  bool checkpointTaken;

    private void Start()
    {
        gm = GameObject.FindGameObjectWithTag("GM").GetComponent<GameMaster>();
    }



    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            gm.lastCheckPointPos = transform.position;
            animator.SetBool("FillUp", true);
            checkpointTaken = true;
        }
    }

   
}
