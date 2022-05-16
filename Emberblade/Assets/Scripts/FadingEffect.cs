using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class FadingEffect : MonoBehaviour
{

    public Tilemap tempFillTileMap;
    public TilemapRenderer rend;

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
        if (collision.gameObject.CompareTag("Player"))
        {
            StartCoroutine("FadeOut");
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            StartCoroutine("FadeIn");
        }
    }

    IEnumerator FadeOut()
    {
        for (float i = 1; i >= -0f; i -= 0.05f)
        {
            Color c = rend.material.color;
            c.a = i;
            rend.material.color = c;
            yield return new WaitForSeconds(0.05f);
        }
    }

    IEnumerator FadeIn()
    {
        for (float i = 0f; i <= 1.05f; i += 0.05f)
        {
            Color c = rend.material.color;
            c.a = i;
            rend.material.color = c;
            yield return new WaitForSeconds(0.05f);
        }
    }


}