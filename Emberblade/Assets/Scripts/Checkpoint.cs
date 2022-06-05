using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour //Detta är skrivet av: Brandon
{
    private GameMaster gm;
    [SerializeField] Animator animator;
    public static bool checkpointTaken;

    private void Start()
    {
        gm = GameObject.FindGameObjectWithTag("GM").GetComponent<GameMaster>();
    }



    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            gm.lastCheckPointPos = transform.position;              //Sparar positionen i GameGaster
            animator.SetBool("FillUp", true);
            checkpointTaken = true;
        }
    }

   
}
