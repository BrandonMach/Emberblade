using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatScript : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField] Animator animator;
    [SerializeField] Transform attackpoint;
    [SerializeField] float attackRange = 0.5f;

    [SerializeField] LayerMask hittableLayers;

    private float timeBetweenAttack = 0.01f;
    public float startTimeBetweenAttack;
    private BoxCollider2D boxCollider;
    private Vector2 idleBoxColliderOffset;


    private void Start()
    {
        boxCollider = GetComponent<BoxCollider2D>();
        boxCollider.offset = idleBoxColliderOffset;
    }

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
                    enemiesToDamage[i].GetComponent<EnemyHealth>().TakeDamage();
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
