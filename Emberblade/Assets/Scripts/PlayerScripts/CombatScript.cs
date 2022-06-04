using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatScript : MonoBehaviour //Detta är skrivet av: Brandon + Sebastian och Philip(animations, fixed crouch)
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
        if (timeBetweenAttack <= 0)// then you can attack
        {
            if (player.isCrouching)
            {
                if (Input.GetKeyDown(KeyCode.J))
                {
                    animator.SetTrigger("SitAttack");
                    soundEffectScript.PlayAttack();

                    Vector3 attackScale = transform.localScale;


                    if (player.lookingRight)
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
                    if (isInBossBattle)
                    {
                        for (int i = 0; i < BossToDamage.Length; i++)
                        {
                            BossToDamage[i].GetComponent<BossHealth>().BossTakeDamage(playerDamage);
                            Debug.Log("We Hit Boss");
                        }
                    }
                    else
                    {
                        for (int i = 0; i < enemiesToDamage.Length; i++)
                        {
                            enemiesToDamage[i].GetComponent<EnemyHealth>().TakeDamage(playerDamage);
                            Debug.Log("We Hit Enemy");
                        }
                    }
                }
            }
            
            else if (Input.GetKeyDown(KeyCode.J))
            {
                animator.SetTrigger("Attack");
                soundEffectScript.PlayAttack();


                Vector3 attackScale = transform.localScale;
                //attackpoint.position = new Vector2(this.transform.position.x + 3.81f, this.transform.position.y + 0.86f); /*= new Vector2(3.81f, 0.86f);*/

                if (player.lookingRight)
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
                if (isInBossBattle)
                {
                    for (int i = 0; i < BossToDamage.Length; i++)
                    {
                        BossToDamage[i].GetComponent<BossHealth>().BossTakeDamage(playerDamage);
                        Debug.Log("We Hit Boss");
                    }
                }
                else
                {
                    for (int i = 0; i < enemiesToDamage.Length; i++)
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

            timeBetweenAttack = startTimeBetweenAttack; //default start value
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
        
        Gizmos.DrawWireSphere(attackpoint.position, attackRange);
    }
}
