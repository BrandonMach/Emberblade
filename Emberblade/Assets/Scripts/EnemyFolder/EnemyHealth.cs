using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{

    public int maxHealth;
    public int health;
    public bool canTakeDamage = true;
    private float startTimeDamageTimer;
    private float damageDelay = 0.5f;
    Currency player;
    public int currency;
    public EnemyHealthbar healthBar;
    
    void Start()
    {
        //health = 3;
        canTakeDamage = true;
        player = GameObject.Find("Player").GetComponent<Currency>();
        health = maxHealth;
        healthBar.SetHealth(health, maxHealth);
    }

    // Update is called once per frame
    void Update()
    {
        DamageWindow();
        EnemyDies();
    }



    public void TakeDamage(int damage)
    {
        if (canTakeDamage)
        {
            health -= damage;
            healthBar.SetHealth(health, maxHealth);
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
            player.IncreaseCurrency(currency);
        }
    }
}
