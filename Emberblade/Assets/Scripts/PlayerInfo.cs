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
            UseEnergy(30);
        }

        if (Input.GetKeyDown(KeyCode.K))
        {
            TakeDamage(20);
        }

        if (Input.GetKey(KeyCode.H))
        {
            if (currentEnergy < maxEnergy)
            {
                currentEnergy++;
                energyBar.SetEnergy(currentEnergy);
            }
            else { currentEnergy = maxEnergy; }
        }

        if (currentHealth <= 0)//Om man får 0 eller mindre health så kör den metoden Death() som man dör av.
        {
            Death();
        }
    }
    
    public void TakeDamage(int damage) //Metod som gör så att man kan förlora health.
    {
        currentHealth -= damage;    
        healthBar.SetHealth(currentHealth);
        if (currentHealth <= 0) { currentHealth = 0; }
    }

    public void Death() //Gör så man kan dö.
    {
        Debug.Log("works");
        Destroy(this.gameObject);
    }

    void UseEnergy(int useEnergy)//Gör så man kan förlora energy.
    {
        currentEnergy -= useEnergy;
        energyBar.SetEnergy(currentEnergy);
        if (currentEnergy <= 0) { currentEnergy = 0; }
    }
}
