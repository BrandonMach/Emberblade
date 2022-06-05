using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour //Detta är skrivet av: Sebastian
{
    private float length, startpos;
    [SerializeField] GameObject cam;
    [SerializeField] float parallaxEffect;

    // Start is called before the first frame update
    void Start()
    {
        startpos = transform.position.x;
        length = GetComponent<SpriteRenderer>().bounds.size.x;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float temp = (cam.transform.position.x * (1 - parallaxEffect)); // Bakgrundbilden ändras beroende på parallax effecten
        float dist = (cam.transform.position.x * parallaxEffect);


        transform.position = new Vector2(startpos + dist, transform.position.y);

        

        if (temp > startpos + length) // Om bakgrundbilden flyttar sig över dess längd, flyttar den parallax bakgrunden framför spelaren
        {
            startpos += length;
        }
        else if (temp < startpos - length)
        {
            startpos -= length;
        }

        
    }
}
