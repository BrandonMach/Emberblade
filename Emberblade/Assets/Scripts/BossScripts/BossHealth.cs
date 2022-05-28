using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossHealth : MonoBehaviour
{
    public int maxHealth;
    public int health;
    public bool canTakeDamage = true;
    private float startTimeDamageTimer;
    private float damageDelay = 0.5f;
    public GameObject abilityItem;
    Currency player;
    public BossHealthBar bossHealthBar;
    public StartBossScript StartBoss;

    void Start()
    {
        //health = 3;
        canTakeDamage = true;
        player = GameObject.Find("Player").GetComponent<Currency>();
        health = maxHealth;
        bossHealthBar.SetHealth(health, maxHealth);
    }

    // Update is called once per frame
    void Update()
    {
        DamageWindow();
        EnemyDies();
        BossHealthBar();
    }

    public void BossHealthBar()
    {
        if (StartBoss.bossAwoke)
        {
            bossHealthBar.SetHealth(health, maxHealth);
        }
    }

    public void BossTakeDamage()
    {
        if (canTakeDamage)
        {
            health--;
            bossHealthBar.SetHealth(health, maxHealth);
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
        if (health <= 0 && startTimeDamageTimer == 0) // if a Boss is killed reveal ne ability pickup
        {
            Destroy(this.gameObject);
            abilityItem.SetActive(true);
            player.IncreaseCurrency(300);
        }
    }
}