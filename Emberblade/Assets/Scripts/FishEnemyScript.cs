using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishEnemyScript : MonoBehaviour
{
    // Start is called before the first frame update

    private Rigidbody2D fish_Rb;
    
    
    void Start()
    {
        fish_Rb = GetComponent<Rigidbody2D>();    
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            fish_Rb.velocity = new Vector2(fish_Rb.velocity.x, 50);
        }
    }
}
