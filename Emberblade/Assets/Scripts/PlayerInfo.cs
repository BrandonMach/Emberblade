using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInfo : MonoBehaviour
{

    public int maxHealth;
    public int currentHealth;
    public HealthBar healthBar;

    // Start is called before the first frame update
    void Start()
    {
        //currentHealth = maxHealth;

        healthBar.SetMaxHealth(maxHealth);
        healthBar.currentHealth(currentHealth);
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyUp(KeyCode.K))
        {
            TakeDamage(20);
        }
    }
    
    void TakeDamage(int damage)
    {
        currentHealth -= damage;    
        healthBar.SetHealth(currentHealth);
    }
}
