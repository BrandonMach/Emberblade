using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour //Detta är skrivet av: Brandon + Sebastian
{

    public int maxHealth;
    public int health;
    public bool canTakeDamage = true;
    private float startTimeDamageTimer;
    private float damageDelay = 0.5f;
    Currency player;
    PlayerInfo playerInfo;
    public int currency;
    [SerializeField] int amoutMana;
    public EnemyHealthbar healthBar;
    
    void Start()
    {
        //health = 3;
        canTakeDamage = true;
        player = GameObject.Find("Player").GetComponent<Currency>();
        playerInfo = GameObject.Find("Player").GetComponent<PlayerInfo>();
        health = maxHealth;
        healthBar.SetHealth(health, maxHealth);
    }

    // Update is called once per frame
    void Update()
    {
        DamageWindow(); //Fiendens Iframe så att han inte tar konstant damage
        EnemyDies(); //Fienden Dör
    }



    public void TakeDamage(int damage) // Fienden Tar skada
    {
        if (canTakeDamage)
        {
            health -= damage;
            healthBar.SetHealth(health, maxHealth);
            canTakeDamage = false;         
        }  
    }

    void DamageWindow()//Fiendens Iframe så att han inte tar konstant damage
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

    void EnemyDies() // Fienden dör
    {
        if (health <= 0 && startTimeDamageTimer == 0)
        {
            Destroy(this.gameObject);
            player.IncreaseCurrency(currency); // Fienden ger spelaren currency
            playerInfo.RechargeEnergy(amoutMana); // Fienden ger spelaren mana
        }
    }
}
