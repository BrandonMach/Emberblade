using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCStoreScript : MonoBehaviour
{
    private GameObject Player, playerMovement;
    private bool triggering, storeIsOpen, isTalking, nextSentence;
    public GameObject text, npcChatTextOb;
    public TMPro.TMP_Text npcText, npcChatText;
    private List<string> responseList = new List<string>();
    public string[] sentences;
    private int wordIndex;
    public float dialogueSpeed;


    private void Start()
    {
        //responseList.Add("Hello!, would you like to see my wares? \n \n \n Press \"E\" to open the Store");
        //responseList.Add("There is no inventory screen yet, press Q to go back");;
        //wordIndex = -1;

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
            else if(wordIndex >= 1 && Input.GetKeyDown(KeyCode.Q) && nextSentence == true)
            {
                wordIndex = 0;
                storeIsOpen = false;
                npcChatTextOb.SetActive(false);
                isTalking=false;

            }
            if (storeIsOpen)
            {
                Player.GetComponent<PlayerControll>().enabled = false;
            }
            else
            {
                Player.GetComponent<PlayerControll>().enabled = true;
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
            Player = other.gameObject;
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
        if (wordIndex >= 1)
        {
            wordIndex = 1;
            storeIsOpen = true;
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
