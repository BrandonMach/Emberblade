using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackChestTwo : MonoBehaviour
{
    [SerializeField] Animator animator;

    [SerializeField] bool trigger;
    public static bool attackChestTwoOpened;
    PlayerControll player;
    NewAbilityTextScript newAbilityText;

    void Start()
    {
        player = GameObject.Find("Player").GetComponent<PlayerControll>();
        newAbilityText = GameObject.Find("NewAbilityController").GetComponent<NewAbilityTextScript>();
    }

    // Update is called once per frame
    void Update()
    {
        if (trigger && !attackChestTwoOpened)
        {
            if (Input.GetKey(KeyCode.E))
            {
                animator.SetTrigger("Open");
                animator.SetBool("IsOpened", true);
                CombatScript.playerDamage += 1;
                newAbilityText.index = 2;
                player.PlayNewAbilityCutscene();
                attackChestTwoOpened = true;
            }

        }

        if (attackChestTwoOpened)
        {
            animator.SetBool("IsOpened", true);
        }

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            trigger = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            trigger = false;
        }
    }
}
