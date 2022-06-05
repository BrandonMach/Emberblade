using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class NPCStoreScript : MonoBehaviour //Detta �r skrivet av: Axel
{
    private GameObject player, playerMovement;
    private bool triggering, storeIsOpen, isTalking, nextSentence;
    public GameObject text, storePanel;
    public TMP_Text npcText, npcChatText, npcName;
    public string[] sentences;
    private int wordIndex;
    public float dialogueSpeed;
    private float speedUpTimer;
    public Animator dialogueAnimator;
    public Animator storeAnimator;
    private bool startDialogue = true;


    private void Start()
    {
        wordIndex = 0;
        nextSentence = true;
    }

    private void Update()
    {
        speedUpTimer += (Time.deltaTime * 1000f);                               //Denna timern anv�nds f�r att kunna snabbspola texten n�r den skrivs ut p� sk�rmen. Har gjort om det till millisekunder.
        if (!isTalking)                                                         //Om man inte redan pratar med NPC:n s� ska det finnas en text �ver hans huvud.
        {
            npcText.text = "Press \"E\" to talk!";

        }

        if (triggering)
        {
            if (isTalking == true)                                             //H�r skriver jag ut NPC:ns namn i dialog rutan.
            {
                text.SetActive(false);
                npcName.text = gameObject.name;
            }
            else
            {
                text.SetActive(true);
            }
            if (Input.GetKeyDown(KeyCode.E))                                  //Trycker man E s� ska man starta konversationen. H�r st�ngs �ven spelarens kontroll �ver karakt�ren av.
            {
                player.GetComponent<PlayerControll>().enabled = false;
                if (startDialogue)
                {
                    dialogueAnimator.SetTrigger("Enter");                     //Detta startar animationen p� dialog-rutan.
                    nextSentence = false;
                    startDialogue = false;
                    text.SetActive(false);
                    isTalking = true;
                    speedUpTimer = 0;
                    NextSentence();                                           //H�r kallas metoden som g�r s� att text faktiskt kommer upp p� sk�rmen.
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
        if (Input.GetKey(KeyCode.E) && !nextSentence && speedUpTimer > 300) //H�ller man inne knappen E medans n�gon pratar, s� ska texten "Snabbspolas". Allts� att det skrivs ut snabbare �n vanligt.
        {
            dialogueSpeed = 0.01f;
        }
        else
        {
            dialogueSpeed = 0.05f;
        }
        if (storeIsOpen)
        {
            if (Input.GetKeyDown(KeyCode.Q)) //Om aff�ren �r �ppen, s� ska skriptet v�nta p� att spelaren vill g� ut ur aff�ren, d� genom att trycka Q. N�r detta h�nder s� ska spelaren f� tillbaka kontrollen, och aff�ren ska st�ngas ner.   
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
        if (wordIndex <= sentences.Length - 1)                          //Om det finns en mening p� det index i arrayen som vi nuvarande �r p�, s� starta WriteSentence().
        {
            npcChatText.text = "";
            StartCoroutine(WriteSentence());
        }
        if (wordIndex > sentences.Length - 1)                           //Om det inte finns fler meningar, �r konversationen klar, och i detta fallet �ppnas en annan sk�rm. En aff�r d�r spelaren kan k�pa saker med hj�lp av knappar.
        {
            nextSentence = false;
            npcChatText.text = "";
            dialogueAnimator.SetTrigger("Exit");                        //avsluta dialoganimationen
            storeAnimator.SetTrigger("Enter");                          //och starta aff�ranimationen.
            storeIsOpen = true;
        }
    }

    IEnumerator WriteSentence()
    {
        foreach (char character in sentences[wordIndex].ToCharArray())      //H�r loopar den igenom den nuvarande menignen, och skriver ut varje bokstav f�r sig. Med en liten paus mellan varje bokstav.
        {
            npcChatText.text += character;
            yield return new WaitForSeconds(dialogueSpeed);
        }
        wordIndex++;
        nextSentence = true;
    }
}
