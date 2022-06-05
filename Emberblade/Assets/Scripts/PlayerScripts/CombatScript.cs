using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatScript : MonoBehaviour //Detta �r skrivet av: Brandon + Sebastian och Philip(animations, fixed crouch)
{
    // Start is called before the first frame update

    [SerializeField] Animator animator;
    [SerializeField] Transform attackpoint;
    [SerializeField] float attackRange = 0.5f;
    private SFXPlaying soundEffectScript;
    [SerializeField] LayerMask hittableLayers;

    private float timeBetweenAttack = 0.01f;
    public float startTimeBetweenAttack;
    private BoxCollider2D boxCollider;
    private Vector2 idleBoxColliderOffset;
    private PlayerControll player;
    public bool isInBossBattle;
    public static int playerDamage = 1;

    private void Start()
    {
        player = GetComponent<PlayerControll>();
        boxCollider = GetComponent<BoxCollider2D>();
        boxCollider.offset = idleBoxColliderOffset;
        soundEffectScript = GameObject.Find("SoundManager").GetComponent<SFXPlaying>();
    }

    // Update is called once per frame
    void Update()
    {
        if (timeBetweenAttack <= 0) // then you can attack
        {
            if (player.isCrouching)
            {
                if (Input.GetKeyDown(KeyCode.J)) //Om spelaren sitter och trycker p� knappen J s� attackerar han samtidigt som han sitter
                {
                    animator.SetTrigger("SitAttack");
                    soundEffectScript.PlayAttack();

                    Vector3 attackScale = transform.localScale;


                    if (player.lookingRight) // Flyttar p� hitboxen samtidigt som spelaren sitter beroende p� om han kollar v�nster och h�ger
                    {
                        attackScale.x *= 1;
                        attackpoint.position = new Vector2(this.transform.position.x + 5, this.transform.position.y - 2.86f);
                    }
                    else
                    {
                        attackScale.x *= -1;
                        attackpoint.position = new Vector2(this.transform.position.x - 5, this.transform.position.y - 2.86f);
                    }

                    Collider2D[] enemiesToDamage = Physics2D.OverlapCircleAll(attackpoint.position, attackRange, hittableLayers);
                    Collider2D[] BossToDamage = Physics2D.OverlapCircleAll(attackpoint.position, attackRange, hittableLayers);


                    if (isInBossBattle) // Om Spelaren m�ter en boss
                    {
                        for (int i = 0; i < BossToDamage.Length; i++) // Sl�r en boss
                        {
                            BossToDamage[i].GetComponent<BossHealth>().BossTakeDamage(playerDamage);
                            Debug.Log("We Hit Boss");
                        }
                    }
                    else
                    {
                        for (int i = 0; i < enemiesToDamage.Length; i++) // Sl�r en vanlig enemy
                        {
                            enemiesToDamage[i].GetComponent<EnemyHealth>().TakeDamage(playerDamage);
                            Debug.Log("We Hit Enemy");
                        }
                    }
                }
            }
            
            else if (Input.GetKeyDown(KeyCode.J)) // Om spelaren inte sitter och trycker p� J attackerar han n�r han st�r upp
            {
                animator.SetTrigger("Attack");
                soundEffectScript.PlayAttack();


                Vector3 attackScale = transform.localScale;
                //attackpoint.position = new Vector2(this.transform.position.x + 3.81f, this.transform.position.y + 0.86f); /*= new Vector2(3.81f, 0.86f);*/

                if (player.lookingRight) // Flyttar p� hitboxen beroende p� om spelaren kollar v�nster och h�ger
                {
                    attackScale.x *= 1;
                    attackpoint.position = new Vector2(this.transform.position.x + 6, this.transform.position.y);
                }
                else
                {
                    attackScale.x *= -1;
                    attackpoint.position = new Vector2(this.transform.position.x - 6, this.transform.position.y);
                }
               

                Collider2D[] enemiesToDamage = Physics2D.OverlapCircleAll(attackpoint.position, attackRange, hittableLayers);
                Collider2D[] BossToDamage = Physics2D.OverlapCircleAll(attackpoint.position, attackRange, hittableLayers);


                if (isInBossBattle) // Om Spelaren m�ter en boss
                {
                    for (int i = 0; i < BossToDamage.Length; i++) // Sl�r en boss
                    {
                        BossToDamage[i].GetComponent<BossHealth>().BossTakeDamage(playerDamage);
                        Debug.Log("We Hit Boss");
                    }
                }
                else
                {
                    for (int i = 0; i < enemiesToDamage.Length; i++) // Sl�r en vanlig enemy
                    {
                        enemiesToDamage[i].GetComponent<EnemyHealth>().TakeDamage(playerDamage);
                        Debug.Log("We Hit Enemy");
                    }
                }
                    
                //for (int i = 0; i < BossToDamage.Length; i++)
                //{
                //    BossToDamage[i].GetComponent<BossHealth>().BossTakeDamage();
                //    enemiesToDamage[i].GetComponent<EnemyHealth>().TakeDamage();
                //    Debug.Log("We Hit ");
                //}


            }

            timeBetweenAttack = startTimeBetweenAttack; //Attack delay s� att spelaren inte spammar attack
        }
        else
        {
            timeBetweenAttack -= Time.deltaTime; //decrease value
        }

    }



    private void OnDrawGizmosSelected()
    {
        if(attackpoint == null)
        {
            return;
        }
        Gizmos.color = Color.cyan;
        
        Gizmos.DrawWireSphere(attackpoint.position, attackRange); // Ritar ut hitboxen i unity
    }
}
