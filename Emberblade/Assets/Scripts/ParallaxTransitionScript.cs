using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxTransitionScript : MonoBehaviour
{
    public GameObject background;
    public GameObject backgroundCave;

    public GameObject player;

    enum backgroundType
    {
        top,
        Cave
    }

    backgroundType currentBackgroundType;

    // Start is called before the first frame update
    void Start()
    {
        currentBackgroundType = backgroundType.top;
    }

    // Update is called once per frame
    void Update()
    {
        BackgroundChange();

        if (currentBackgroundType == backgroundType.top && player.transform.position.y > transform.position.y)
        {
            currentBackgroundType = backgroundType.Cave;
        }
        if (currentBackgroundType == backgroundType.Cave && player.transform.position.y < transform.position.y)
        {
            currentBackgroundType = backgroundType.top;
        }
        
    }

    private void BackgroundChange()
    {
        if (currentBackgroundType == backgroundType.top)
        {
            background.SetActive(false);
            backgroundCave.SetActive(true);
        }
        if (currentBackgroundType == backgroundType.Cave)
        {
            background.SetActive(true);
            backgroundCave.SetActive(false);
        }
    }

    //private void OnTriggerEnter2D(Collider2D collision)
    //{
    //    if (collision.gameObject.CompareTag("Player") && currentBackgroundType == backgroundType.top && player.transform.position.y > transform.position.y)
    //    {
    //        background.SetActive(false);
    //        backgroundCave.SetActive(true);
    //        currentBackgroundType = backgroundType.Cave;
    //    }
    //    if (collision.gameObject.CompareTag("Player") && currentBackgroundType == backgroundType.Cave && player.transform.position.y < transform.position.y)
    //    {
    //        background.SetActive(true);
    //        backgroundCave.SetActive(false);
    //        currentBackgroundType = backgroundType.top;
    //    }
    //}

    
}
