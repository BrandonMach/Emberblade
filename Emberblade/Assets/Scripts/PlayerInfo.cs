using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInfo : MonoBehaviour
{

    public int maxHealth;
    public int currentHealth;
    public int maxEnergy;
    public int currentEnergy; 
    public HealthBar healthBar;
    public EnergyBar energyBar;

    // Start is called before the first frame update
    void Start()
    {
        //currentHealth = maxHealth;

        healthBar.SetMaxHealth(maxHealth);
        healthBar.currentHealth(currentHealth);
        energyBar.SetMaxEnergy(maxEnergy);
        energyBar.currentEnergy(currentEnergy);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            UseEnergy(10);
        }

        if (Input.GetKeyUp(KeyCode.K))
        {
            TakeDamage(20);
        }

        if (currentHealth <= 0)
        {
            Death();
        }
    }
    
    public void TakeDamage(int damage)
    {
        currentHealth -= damage;    
        healthBar.SetHealth(currentHealth);
    }

    public void Death()
    {
        Debug.Log("works");
        Destroy(this.gameObject);
    }

    void UseEnergy(int useEnergy)
    {
        currentEnergy -= useEnergy;
        energyBar.SetEnergy(currentEnergy);
    }
}
