using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerInfo : MonoBehaviour //Detta ?r skrivet av: Philip + Brandon(parry) + Sebastian(IFrame)
{
    [Header("IFrame")]
    public Color damageFlashColor;
    public Color regularColor;
    private float damageFlashDuration = 0.1f;
    private int numberOfDamageFlashes = 8;
    public SpriteRenderer sprite;

    [Header ("Parry")]
    public Color parryFlashColor;
    public float parryFlashDuration;
    [SerializeField] int numberOfParryFlashes;
    private SFXPlaying sfxScript;

    [Header ("UI")]
    public static int maxHealth = 200;
    public static int currentHealth;
    public static int maxEnergy = 150;
    public static int currentEnergy;
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
        if (Input.GetKeyDown(KeyCode.I)) //Om man klickar p? knapp s? f?rlorar man liv.
        {
            TakeDamage(20);
        }

        if (Input.GetKey(KeyCode.O)) //Om man klickar p? knapp s? f?r man tillbaka energi.
        {
            RechargeEnergy(1);
        }

        if (Input.GetKeyDown(KeyCode.U)) //Om man klickar p? knapp s? f?r man tillbaka liv.
        {
            Heal(35);
        }

        if (currentHealth <= 0)//Om man f?r 0 eller mindre health s? k?r den metoden Death() som man d?r av.
        {
            Death();
        }

        if (unlockedManaRegen) 
        {
            RechargeEnergyOverTime();
        }
        
    }
    
   

    public void TakeDamage(int damage) //Metod som g?r s? att man kan f?rlora health.
    {
        if (!PlayerControll.isParrying && canTakeDamage)            //Om spelarn parrerar star man ingen skada
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
        else if(PlayerControll.isParrying) 
        {
            sfxScript.PlayParry();
            StartCoroutine(ParryFlash());
            Debug.LogError("Parry Succes");
           
        }
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

    public void RechargeEnergyOverTime() //G?r s? att man f?r energi tillbaka genom tid.
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

    public void RechargeEnergy(int amount) //G?r s? att man f?r tillbaka energi.
    {
        if (currentEnergy < maxEnergy)
        {
            currentEnergy += amount;
            energyBar.SetEnergy(currentEnergy);
        }
        else { currentEnergy = maxEnergy; }


    }

    public void Heal(int health) //Metod som g?r att man kan f? tillbaka health.
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

    public void Death() //G?r s? man kan d?.
    {
        
        Debug.Log("works");
        
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        currentHealth = maxHealth;
        currentEnergy = maxEnergy;
        Destroy(this.gameObject);
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
    public void UseEnergy(int energy)
    {
        currentEnergy -= energy;
        energyBar.SetEnergy(currentEnergy);
    }

}
