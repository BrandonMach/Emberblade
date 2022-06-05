using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackChestOne : MonoBehaviour //Detta är skrivet av: Sebastian
{
    [SerializeField] Animator animator;

    [SerializeField] bool trigger;
    public static bool attackChestOneOpened;
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
        if (trigger && !attackChestOneOpened)// Om Spelaren är nära kistan och om han inte har tagit den förr
        {
            if (Input.GetKey(KeyCode.E))
            {
                animator.SetTrigger("Open");
                animator.SetBool("IsOpened", true);
                CombatScript.playerDamage += 1; // Ökar spelaren skada mot fiender med 1 
                player.OpenChestCutScene();
                newAbilityText.index = 2;
                attackChestOneOpened = true;
            }

        }

        if (attackChestOneOpened)// Om spelaren har öppnat kistan förr
        {
            animator.SetBool("IsOpened", true);
        }

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player")) // Spelaren är nära kistan
        {
            trigger = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)// Spelaren är inte nära kistan
    {
        if (other.gameObject.CompareTag("Player"))
        {
            trigger = false;
        }
    }
}
