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
    private int wordIndex;
    public float dialogueSpeed;
    public Animator dialogueAnimator;
    public Animator animator;
    private bool startDialogue = true;


    private void Start()
    {
        wordIndex = 0;
        nextSentence = true;
    }

    private void Update()
    {
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
                    startDialogue = false;
                    text.SetActive(false);
                    isTalking = true;
                    NextSentence();
                }
                else
                {
                    if (nextSentence == true)
                    {
                        nextSentence = false;
                        NextSentence();
                    }
                }

            }
        }
        else
        {
            text.SetActive(false);
        }

        if (storeIsOpen)
        {
            storePanel.SetActive(true);
            if (Input.GetKeyDown(KeyCode.Q))
            {
                storeIsOpen = false;
                storePanel.gameObject.SetActive(false);
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
