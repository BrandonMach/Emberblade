using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class NewAbilityTextScript : MonoBehaviour //Detta är skrivet av: Brandon
{

    // Update is called once per frame
    public TextMeshProUGUI abilityText;
    public string[] sentences;
    public int index = 0;
    public float textSpeed;
    public Animator textAnimator;
    public bool startText = false;
    public TMP_Text playerName;

    // Start is called before the first frame update


    // Update is called once per frame
    void Update()
    {
        if (startText)
        {
            StartCoroutine(WriteSentence());
            playerName.text = "Groodan";
            textAnimator.SetTrigger("Enter");
            startText = false;
        }
       
    }
    public void ClosePopUP()
    {
        textAnimator.SetTrigger("Exit");
        Invoke("ClearText", 1);
    }
    private void ClearText()
    {
        abilityText.text = "";
    }

    IEnumerator WriteSentence()
    {
        abilityText.text = sentences[index];
        yield return new WaitForSeconds(textSpeed);
    }

}
