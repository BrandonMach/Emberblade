using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    private GameObject triggeringNpc;
    private bool triggering;
    private bool isTalking;
    public GameObject Text;
    public TMPro.TMP_Text npcText;
    private List<string> responseList = new List<string>();


    private void Start()
    {
        responseList.Add("Lovely weather we're having!");
        responseList.Add("Hello, nice to meet you!");
        responseList.Add("Are you okay?");
        responseList.Add("I'm starting to feel sick...");
        responseList.Add("You feel an evil presence watching you...");
        responseList.Add("I really don't like your shirt!");
    }

    private void Update()
    {
        if (!isTalking)
        {
            npcText.text = "Press \"E\" to talk!";
        }

        if (triggering)
        {
            Text.SetActive(true);

            if (Input.GetKeyDown(KeyCode.E))
            {
                isTalking = true;
                npcText.text = responseList[Random.Range(0, responseList.Count)];
            }
        }
        else
        {
            Text.SetActive(false);
        }
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "NPC")
        {
            triggering = true;
            triggeringNpc = other.gameObject;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "NPC")
        {
            triggering = false;
            triggeringNpc = null;
            isTalking = false;
        }
    }
}
