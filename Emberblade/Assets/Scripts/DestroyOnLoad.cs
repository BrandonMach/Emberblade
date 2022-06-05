using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOnLoad : MonoBehaviour //Detta är skrivet av: Brandon
{
    // Start is called before the first frame update

    public static bool alive;
    private static DestroyOnLoad instance;
    public Vector2 lastCheckPointPos;

    private void Update()
    {
        if (!alive)
        {
            Destroy(gameObject);
        }
    }

    //private void Awake()
    //{
    //    if (instance == null)
    //    {
    //        Destroy(gameObject);
    //    }
    //}
}
