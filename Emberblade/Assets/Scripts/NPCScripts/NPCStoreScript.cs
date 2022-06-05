using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class NPCStoreScript : MonoBehaviour //Detta är skrivet av: Axel
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
        speedUpTimer += (Time.deltaTime * 1000f);                               //Denna timern används för att kunna snabbspola texten när den skrivs ut på skärmen. Har gjort om det till millisekunder.
        if (!isTalking)                                                         //Om man inte redan pratar med NPC:n så ska det finnas en text över hans huvud.
        {
            npcText.text = "Press \"E\" to talk!";

        }

        if (triggering)
        {
            if (isTalking == true)                                             //Här skriver jag ut NPC:ns namn i dialog rutan.
            {
                text.SetActive(false);
                npcName.text = gameObject.name;
            }
            else
            {
                text.SetActive(true);
            }
            if (Input.GetKeyDown(KeyCode.E))                                  //Trycker man E så ska man starta konversationen. Här stängs även spelarens kontroll över karaktären av.
            {
                player.GetComponent<PlayerControll>().enabled = false;
                if (startDialogue)
                {
                    dialogueAnimator.SetTrigger("Enter");                     //Detta startar animationen på dialog-rutan.
                    nextSentence = false;
                    startDialogue = false;
                    text.SetActive(false);
                    isTalking = true;
                    speedUpTimer = 0;
                    NextSentence();                                           //Här kallas metoden som gör så att text faktiskt kommer upp på skärmen.
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
        if (Input.GetKey(KeyCode.E) && !nextSentence && speedUpTimer > 300) //Håller man inne knappen E medans någon pratar, så ska texten "Snabbspolas". Alltså att det skrivs ut snabbare än vanligt.
        {
            dialogueSpeed = 0.01f;
        }
        else
        {
            dialogueSpeed = 0.05f;
        }
        if (storeIsOpen)
        {
            if (Input.GetKeyDown(KeyCode.Q)) //Om affären är öppen, så ska skriptet vänta på att spelaren vill gå ut ur affären, då genom att trycka Q. När detta händer så ska spelaren få tillbaka kontrollen, och affären ska stängas ner.   
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
        if (wordIndex <= sentences.Length - 1)                          //Om det finns en mening på det index i arrayen som vi nuvarande är på, så starta WriteSentence().
        {
            npcChatText.text = "";
            StartCoroutine(WriteSentence());
        }
        if (wordIndex > sentences.Length - 1)                           //Om det inte finns fler meningar, är konversationen klar, och i detta fallet öppnas en annan skärm. En affär där spelaren kan köpa saker med hjälp av knappar.
        {
            nextSentence = false;
            npcChatText.text = "";
            dialogueAnimator.SetTrigger("Exit");                        //avsluta dialoganimationen
            storeAnimator.SetTrigger("Enter");                          //och starta affäranimationen.
            storeIsOpen = true;
        }
    }

    IEnumerator WriteSentence()
    {
        foreach (char character in sentences[wordIndex].ToCharArray())      //Här loopar den igenom den nuvarande menignen, och skriver ut varje bokstav för sig. Med en liten paus mellan varje bokstav.
        {
            npcChatText.text += character;
            yield return new WaitForSeconds(dialogueSpeed);
        }
        wordIndex++;
        nextSentence = true;
    }
}
