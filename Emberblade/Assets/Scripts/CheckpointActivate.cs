using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointActivate : MonoBehaviour //Detta ?r skrivet av: Brandon + Axel
{

    [SerializeField] bool triggering;
    [SerializeField] Animator animator;
    private GameMaster gm;


    // Start is called before the first frame update
    void Start()
    {
        gm = GameObject.FindGameObjectWithTag("GM").GetComponent<GameMaster>();
    }

    // Update is called once per frame
    void Update()
    {

        if (triggering)
        {
            if (Input.GetKeyDown(KeyCode.E))                    //N?r man trycker E och aktiverar en checkpoint, s? ska en animation starta f?r checkpointen.
            {
                animator.SetTrigger("Entry");
                gm.lastCheckPointPos = transform.position;
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
