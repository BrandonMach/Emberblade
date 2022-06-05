using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpRing : MonoBehaviour //Detta är skrivet av: Brandon
{

    // Update is called once per frame
    void Update()
    {
        Object.Destroy(gameObject, 0.2f);
    }
}
