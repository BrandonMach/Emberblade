using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class FadingEffect : MonoBehaviour //Detta är skrivet av: Sebastian
{

    [SerializeField] Tilemap tempFillTileMap;
    [SerializeField] TilemapRenderer rend;


    private void OnTriggerEnter2D(Collider2D collision) //Om Spelaren är inne i området
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            StartCoroutine("FadeOut");
        }
    }

    private void OnTriggerExit2D(Collider2D collision) //Om Spelaren är utanför området
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            StartCoroutine("FadeIn");
        }
    }

    IEnumerator FadeOut() //Det som gömde den hemliga vägen blir osynlig
    {
        for (float i = 1; i >= -0.05f; i -= 0.05f)
        {
            Color c = rend.material.color;
            c.a = i;
            rend.material.color = c;
            yield return new WaitForSeconds(0.05f);
        }
    }

    IEnumerator FadeIn()//Det som gömde den hemliga vägen blir synlig
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
