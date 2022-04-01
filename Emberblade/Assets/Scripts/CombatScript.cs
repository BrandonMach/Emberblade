using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatScript : MonoBehaviour
{
    // Start is called before the first frame update

    public Animator animator;
    public Transform attackpoint;
    public float attackRange = 0.5f;

    public LayerMask hittableLayers;

    private float timeBetweenAttack;
    public float startTimeBetweenAttack;

    
   

    

    // Update is called once per frame
    void Update()
    {
        if (timeBetweenAttack <= 0)// than you can attack
        {
            
            if (Input.GetKey(KeyCode.Backspace))
            {
                animator.SetTrigger("Attack");
                Collider2D[] enemiesToDamage = Physics2D.OverlapCircleAll(attackpoint.position, attackRange, hittableLayers);

                for (int i = 0; i < enemiesToDamage.Length; i++)
                {
                    //enemiesToDamage[i].GetComponent<Enemy>().TakeDamage(damage);
                    Debug.Log("We Hit ");
                }
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
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(attackpoint.position, attackRange);
    }
}
