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
    public float dialogueSpeed;


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
              
                text.SetActive(false);
                npcChatTextOb.SetActive(true);
                isTalking = true;
                
                if (nextSentence == true)
                {
                    nextSentence = false;
                    npcChatText.text = "";
                    StartCoroutine(WriteSentence());
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


    IEnumerator WriteSentence()
    {
        if (wordIndex > sentences.Length - 1)
        {
            wordIndex = 2;
        }
        foreach (char character in sentences[wordIndex].ToCharArray())
        {
            npcChatText.text += character;
            yield return new WaitForSeconds(dialogueSpeed);
        }
        wordIndex++;
        nextSentence = true;
    }
}
