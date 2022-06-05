using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackChestTwo : MonoBehaviour //Detta �r skrivet av: Sebastian
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
                CombatScript.playerDamage += 1;// �kar spelaren skada mot fiender med 1 
                newAbilityText.index = 2;
                player.OpenChestCutScene();
                attackChestTwoOpened = true;
            }

        }

        if (attackChestTwoOpened)// Om spelaren har �ppnat kistan f�rr
        {
            animator.SetBool("IsOpened", true);
        }

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))// Spelaren �r n�ra kistan
        {
            trigger = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)// Spelaren �r inte n�ra kistan
    {
        if (other.gameObject.CompareTag("Player"))
        {
            trigger = false;
        }
    }
}
