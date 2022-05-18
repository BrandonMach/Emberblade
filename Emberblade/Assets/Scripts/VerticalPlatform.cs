using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VerticalPlatform : MonoBehaviour
{
    private PlatformEffector2D effector;
    private float waitTime;
    private void Start()
    {
        effector = GetComponent<PlatformEffector2D>();
    }

    private void Update()
    {
        waitTime += Time.deltaTime;
        if (Input.GetKeyDown(KeyCode.S))
        {
            waitTime = 0f;
            effector.rotationalOffset = 180f;
        }
        else if (waitTime >= 1f)
        {
            effector.rotationalOffset = 0f;
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            effector.rotationalOffset = 0f;
        }
    }

}
