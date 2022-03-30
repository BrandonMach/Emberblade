using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    // Start is called before the first frame update

    public float timeBetweenAttack;
    private float startTimeBetweenAttack;

    public Transform attackPos;
    public float attackRange;
    public LayerMask whatIsEnemies;
    public int damage;
    public Animator animator;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        //Attack Cooldown
        if (timeBetweenAttack <= 0)
        {
            if (Input.GetKey(KeyCode.U))
            {
                animator.SetBool("IsAttacking", true);
                Collider2D[] enemiesToDamage = Physics2D.OverlapCircleAll(attackPos.position, attackRange, whatIsEnemies);
                foreach (Collider2D enemy in enemiesToDamage)
                {
                    timeBetweenAttack += Time.deltaTime;
                    Debug.Log("We hit" + enemy.name);
                    animator.SetBool("IsAttacking", false);
                }
            }
            timeBetweenAttack = startTimeBetweenAttack;
        }
        else
        {
            timeBetweenAttack -= Time.deltaTime;
           
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPos.position, attackRange);
    }
}
