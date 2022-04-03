using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{


    public int health;
    public bool canTakeDamage = true;
    private float startTimeDamageTimer;
    private float damageDelay = 0.5f;
    public Animator animator;
    void Start()
    {
        health = 3;
        canTakeDamage = true;

    }

    // Update is called once per frame
    void Update()
    {
        DamageWindow();
        EnemyDies();
        if (Input.GetKeyDown(KeyCode.M))
        {
            animator.SetBool("Attacking", true);
        }
       
    }


    public void TakeDamage()
    {
        if (canTakeDamage)
        {
            health--;
            canTakeDamage = false;         
        }  
    }

    void DamageWindow()
    {
        if (!canTakeDamage)
        {
            startTimeDamageTimer += Time.deltaTime;
        }
        if (startTimeDamageTimer >= damageDelay)
        {
            canTakeDamage = true;
            startTimeDamageTimer = 0;
        }
    }

    void EnemyDies()
    {
        if (health <= 0 && startTimeDamageTimer == 0)
        {
            Destroy(this.gameObject);
        }
    }
}
