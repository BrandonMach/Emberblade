using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInfo : MonoBehaviour
{

    int maxHealth;
    public int currentHealth;
    int maxEnergy;
    public int currentEnergy; 
    public HealthBar healthBar;
    public EnergyBar energyBar;

    // Start is called before the first frame update
    void Start()
    {
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

        if (currentHealth <= 0)//Om man f�r 0 eller mindre health s� k�r den metoden Death() som man d�r av.
        {
            Death();
        }
    }
    
    public void TakeDamage(int damage) //Metod som g�r s� att man kan f�rlora health.
    {
        currentHealth -= damage;    
        healthBar.SetHealth(currentHealth);
    }

    public void Death() //G�r s� man kan d�.
    {
        Debug.Log("works");
        Destroy(this.gameObject);
    }

    void UseEnergy(int useEnergy)//G�r s� man kan f�rlora energy.
    {
        currentEnergy -= useEnergy;
        energyBar.SetEnergy(currentEnergy);
    }
}
