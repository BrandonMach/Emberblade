using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SwitchScene : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && this.gameObject.name == "GoToDesert")
        {
            SwitchToCave();
        }
        if (collision.gameObject.CompareTag("Player") && this.gameObject.name == "ReturnToSwamp")
        {
            SwitchSwamp();
        }

    }

    public void SwitchSwamp()
    {
        SceneManager.LoadScene(2);
    }
    public void SwitchToCave()
    {
        SceneManager.LoadScene(1);
    }
}
