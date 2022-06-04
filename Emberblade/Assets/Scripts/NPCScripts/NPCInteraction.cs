using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class NPCInteraction : MonoBehaviour //Detta är skrivet av: Axel
{
    private bool triggering, isTalking, nextSentence;
    public GameObject text;
    public TMP_Text npcText, npcChatText, npcName;
    public string[] sentences;
    private int wordIndex;
    private GameObject player;
    public float dialogueSpeed;
    private float speedUpTimer;
    public Animator dialogueAnimator;
    private bool startDialogue = true;


    private void Start()
    {
        wordIndex = 0;
        nextSentence = true;
    }

    private void Update()
    {
        speedUpTimer += (Time.deltaTime * 1000f);
        if (!isTalking)
        {
            npcText.text = "Press \"E\" to talk!";
        }

        if (triggering)
        {
            if (isTalking == true)
            {
                text.SetActive(false);
                npcName.text = gameObject.name;
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
                    nextSentence = false;
                    startDialogue = false;
                    text.SetActive(false);
                    isTalking = true;
                    speedUpTimer = 0;
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
