using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class NPCStoreScript : MonoBehaviour
{
    private GameObject player, playerMovement;
    private bool triggering, storeIsOpen, isTalking, nextSentence;
    public GameObject text, storePanel;
    public TMP_Text npcText, npcChatText, npcName;
    public string[] sentences;
    public int wordIndex;
    private float speedUpTimer;
    public float dialogueSpeed;
    public Animator dialogueAnimator;
    public Animator storeAnimator;
    public Animator animator;
    private bool startDialogue = true;


    private void Start()
    {
        wordIndex = 0;
        nextSentence = true;
    }

    private void Update()
    {
        speedUpTimer += (Time.deltaTime * 1000f);
        //if (!isTalking)
        //{
        //    npcText.text = "Press \"E\" to talk!";
        //    animator.SetBool("Money", false);
        //}

        if (triggering)
        {
            if (isTalking == true)
            {
                text.SetActive(false);
                npcName.text = gameObject.name;
                animator.SetBool("Money", true);
            }
            else
            {
                text.SetActive(true);
            }
            if (Input.GetKeyDown(KeyCode.E))
            {
                player.GetComponent<PlayerControll>().enabled = false;
                player.transform.position = transform.position + new Vector3(15, -2, 0);
               
                if (startDialogue)
                {
                    dialogueAnimator.SetTrigger("Enter");
                    nextSentence = false;
                    startDialogue = false;
                    text.SetActive(false);
                    speedUpTimer = 0;
                    isTalking = true;
                    NextSentence();
                }
                else
                {
                    if (nextSentence == true)
                    {
                        nextSentence = false;
                        NextSentence();
                        speedUpTimer = 0;
                    }
                }

            }
        }
        else
        {
            text.SetActive(false);
        }

        if (Input.GetKey(KeyCode.E) && !nextSentence && speedUpTimer > 300)
        {
            dialogueSpeed = 0.01f;

        }
        else
        {
            dialogueSpeed = 0.05f;
        }

        if (storeIsOpen)
        {
            if (Input.GetKeyDown(KeyCode.Q))
            {
                storeIsOpen = false;
                storeAnimator.SetTrigger("Exit");
                wordIndex = 0;
                startDialogue = true;
                player.GetComponent<PlayerControll>().enabled = true;
            }
        }
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag( "Player"))
        {
            triggering = true;
            player = other.gameObject;
            isTalking = false;
            Debug.Log("Store in range");
            npcText.text = "Press \"E\" to talk!";
            animator.SetBool("Money", false);
        }
    }

    //private void OnTriggerExit2D(Collider2D other)
    //{
    //    if (other.tag == "Player")
    //    {
    //        triggering = false;
    //        isTalking = false;
    //    }
    //}

    void NextSentence()
    {
        if (wordIndex <= sentences.Length - 1)
        {
            npcChatText.text = "";
            StartCoroutine(WriteSentence());
        }
        if (wordIndex > sentences.Length - 1)
        {
            nextSentence = false;
            npcChatText.text = "";
            dialogueAnimator.SetTrigger("Exit");
            storeAnimator.SetTrigger("Enter");
            storeIsOpen = true;
            //wordIndex = 0;
            //startDialogue = true;
            //player.GetComponent<PlayerControll>().enabled = true;
        }
    }

    IEnumerator WriteSentence()
    {
        foreach (char character in sentences[wordIndex].ToCharArray())
        {
            npcChatText.text += character;
            yield return new WaitForSeconds(dialogueSpeed);
        }
        wordIndex++;
        nextSentence = true;
    }
}
