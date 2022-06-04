using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerInfo : MonoBehaviour
{
    [Header("IFrame")]
    public Color damageFlashColor = new Color(255, 255, 255, 165);
    public Color regularColor = new Color(255, 255, 255, 255);
    private float damageFlashDuration = 0.1f;
    private int numberOfDamageFlashes = 8;
    public SpriteRenderer sprite;

    [Header ("Parry")]
    public Color parryFlashColor;
    public float parryFlashDuration;
    [SerializeField] int numberOfParryFlashes;
    private SFXPlaying sfxScript;

    [Header ("UI")]
    public int maxHealth;
    public int currentHealth;
    public int maxEnergy;
    public int currentEnergy;
    public static bool unlockedManaRegen;
    private float timeManaRegen;
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
        sfxScript = GameObject.Find("SoundManager").GetComponent<SFXPlaying>();
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

        if (unlockedManaRegen)
        {
            RechargeEnergyOverTime();
        }
        
    }
    
   

    public void TakeDamage(int damage) //Metod som gör så att man kan förlora health.
    {
        if (!playerControllScript.isParrying && canTakeDamage)
        {
            currentHealth -= damage;
            healthBar.SetHealth(currentHealth);
            StartCoroutine(DamageFlash());
            //canTakeDamage = false;
            if (currentHealth <= 0)
            {
                currentHealth = 0;
            }
        }
        else if(playerControllScript.isParrying)
        {
            sfxScript.PlayParry();
            StartCoroutine(ParryFlash());
            Debug.LogError("Parry Succes");
           
        }
        //if (canTakeDamage)
        //{
        //    //playerControllScript.knockbackCount = 10;
        //    currentHealth -= damage;
        //    healthBar.SetHealth(currentHealth);
        //    StartCoroutine(FlashCo());
        //    //canTakeDamage = false;
        //}
        //if (currentHealth <= 0)
        //{
        //    currentHealth = 0;
        //}
    }


    private IEnumerator DamageFlash()
    {
        int temp = 0;
        canTakeDamage = false;
        while (temp < numberOfDamageFlashes)
        {
            sprite.color = damageFlashColor;
            yield return new WaitForSeconds(damageFlashDuration);
            sprite.color = regularColor;
            yield return new WaitForSeconds(damageFlashDuration);
            temp++;
        }
        canTakeDamage = true;
    }
    private IEnumerator ParryFlash()
    {
        int temp = 0;
        while (temp < numberOfParryFlashes)
        {
            sprite.color = parryFlashColor;
            yield return new WaitForSeconds(parryFlashDuration);
            sprite.color = regularColor;
            yield return new WaitForSeconds(parryFlashDuration);
            temp++;
        }
    }

    public void RechargeEnergyOverTime()
    {
        if (currentEnergy < maxEnergy)
        {
            timeManaRegen += Time.deltaTime;
            if (timeManaRegen >= 10)
            {
                currentEnergy += 10;
                energyBar.SetEnergy(currentEnergy);
                timeManaRegen = 0;
            }
        }
        else { currentEnergy = maxEnergy; }
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
