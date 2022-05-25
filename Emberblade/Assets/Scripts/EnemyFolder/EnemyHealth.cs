using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{

    public int maxHealth;
    public int health;
    [SerializeField] bool canTakeDamage = true;
    private float startTimeDamageTimer;
    private float damageDelay = 0.5f;
    [SerializeField] GameObject abilityItem;
    Currency player;
    [SerializeField] EnemyHealthbar healthBar;
    
    void Start()
    {
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


    public void TakeDamage()
    {
        if (canTakeDamage)
        {
            health--;
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
        if (health <= 0 && startTimeDamageTimer == 0 && this.gameObject.CompareTag("Boss")) // if a Boss is killed reveal ne ability pickup
        {
            Destroy(this.gameObject);
            abilityItem.SetActive(true);
            player.IncreaseCurrency(300);

        }

        else if (health <= 0 && startTimeDamageTimer == 0)
        {
            Destroy(this.gameObject);
        }
    }
}
