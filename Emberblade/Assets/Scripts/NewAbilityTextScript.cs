using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class NewAbilityTextScript : MonoBehaviour
{

    public TextMeshProUGUI abilityText;
    public string[] sentences;
    private int index = 0;
    public float textSpeed;
    public Animator testAnimator;
    private bool startText = true;
    // Start is called before the first frame update


    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.L))
        {
            if (startText)
            {
                testAnimator.SetTrigger("Enter");
                startText = false;
                
            }
            else
            {
                NextSentence();
            }
           
        }
    }

    void NextSentence()
    {
        if(index <= sentences.Length - 1)
        {
            abilityText.text = "";
            StartCoroutine(WriteSentence());
        }
        else
        {
            abilityText.text = "";
            testAnimator.SetTrigger("Exit");
            index = 0;
            startText = true;
        }
    }

    IEnumerator WriteSentence()
    {
        foreach (char character in sentences[index].ToCharArray())
        {
            abilityText.text += character;
            yield return new WaitForSeconds(textSpeed);
        }

        index++;
        
    }
}
