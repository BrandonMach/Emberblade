using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerInfo : MonoBehaviour
{

    public int maxHealth;
    public int currentHealth;
    public int maxEnergy;
    public int currentEnergy;
    [SerializeField] HealthBar healthBar;
    [SerializeField] EnergyBar energyBar;
    public bool canTakeDamage;
    private float startTimeDamageTimer;
    private PlayerControll playerControllScript;
    LayerMask enemyLayer;

    private GameMaster gm;

    // Start is called before the first frame update
    void Start()
    {
        healthBar.SetMaxHealth(maxHealth);
        healthBar.currentHealth(currentHealth);
        energyBar.SetMaxEnergy(maxEnergy);
        energyBar.currentEnergy(currentEnergy);
        playerControllScript = GetComponent<PlayerControll>();

        gm = GameObject.FindGameObjectWithTag("GM").GetComponent<GameMaster>();
        if (Checkpoint.checkpointTaken)
        {
            transform.position = gm.lastCheckPointPos;
        }
        canTakeDamage = true;
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            TakeDamage(20);
        }

        if (Input.GetKey(KeyCode.O))
        {
            RechargeEnergy();
        }

        if (Input.GetKeyDown(KeyCode.U))
        {
            Heal(35);
        }

        if (currentHealth <= 0)//Om man får 0 eller mindre health så kör den metoden Death() som man dör av.
        {
            Death();
        }
        DamageWindow();
    }
    
    public void TakeDamage(int damage) //Metod som gör så att man kan förlora health.
    {
        if (!playerControllScript.isParrying)
        {
            currentHealth -= damage;
            healthBar.SetHealth(currentHealth);
            canTakeDamage = false;
            if (currentHealth <= 0)
            {
                currentHealth = 0;
            }
        }
        else
        {
            Debug.Log("Parry Succes");
        }
        if (canTakeDamage)
        {
            //playerControllScript.knockbackCount = 10;
            currentHealth -= damage;
           
            

            healthBar.SetHealth(currentHealth);
            canTakeDamage = false;
        }
       
        if (currentHealth <= 0)
        {
            currentHealth = 0;
        }
    }

    public void RechargeEnergy()
    {
        if (currentEnergy < maxEnergy)
        {
            currentEnergy++;
            energyBar.SetEnergy(currentEnergy);
        }
        else { currentEnergy = maxEnergy; }
    }

    public void Heal(int health) //Metod som gör att man kan få tillbaka health.
    {
        if (currentEnergy >= health && currentHealth < maxHealth)
        {
            currentHealth += health;
            currentEnergy -= health;
            healthBar.SetHealth(currentHealth);
            energyBar.SetEnergy(currentEnergy);
            if (currentHealth >= maxHealth) 
            { 
                currentHealth = maxHealth; 
            }
        }
    }

    private void OnParticleCollision(GameObject other) //Poison rain
    {
        Debug.Log("Poison");
        TakeDamage(2);
    }

    public void Death() //Gör så man kan dö.
    {
        Debug.Log("works");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        currentHealth = maxHealth;
        Destroy(this.gameObject);
    }

    public void UseEnergy(int energy)
    {
        currentEnergy -= energy;
        energyBar.SetEnergy(currentEnergy);
    }

    public void DamageWindow()
    {
        if (!canTakeDamage)
        {
            startTimeDamageTimer += Time.deltaTime;
        }
        if (startTimeDamageTimer >= 1)
        {
            canTakeDamage = true;
            startTimeDamageTimer = 0;
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        
        if (other.transform.position.x < transform.position.x) 
        {
            playerControllScript.knockFromRight = false;
        }
        else
        {
            playerControllScript.knockFromRight = true;
        }
    }
}
