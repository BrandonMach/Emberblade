using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class NPCInteraction : MonoBehaviour
{
    private bool triggering, isTalking, nextSentence;
    public GameObject text, npcChatTextOb;
    public TMP_Text npcText, npcChatText;
    public string[] sentences;
    private int wordIndex;
    private GameObject player;
    public float dialogueSpeed;
    public Animator dialogueAnimator;
    private bool startDialogue = true;


    private void Start()
    {
        wordIndex = 0;
        nextSentence = true;
    }

    private void Update()
    {
        if (!isTalking)
        {
            npcText.text = "Press \"E\" to talk!";
        }

        if (triggering)
        {
            if (isTalking == true)
            {
                text.SetActive(false);
            }
            else
            {
                text.SetActive(true);
            }
            if (Input.GetKeyDown(KeyCode.E))
            {
                player.GetComponent<PlayerControll>().enabled = false;
                if (startDialogue)
                {
                    dialogueAnimator.SetTrigger("Enter");
                    startDialogue = false;
                    text.SetActive(false);
                    isTalking = true;
                    npcChatTextOb.SetActive(true);
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
            npcChatTextOb.SetActive(false);
        }
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            triggering = true;
            player = other.gameObject;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            triggering = false;
            isTalking = false;
        }
    }


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
            wordIndex = 2;
            startDialogue = true;
            player.GetComponent<PlayerControll>().enabled = true;
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
