using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManaRegenChest : MonoBehaviour //Detta är skrivet av: Sebastian
{
    [SerializeField] Animator animator;

    [SerializeField] bool trigger;
    public static bool manaRegenChestOpened;
    NewAbilityTextScript newAbilityText;
    PlayerControll player;

    void Start()
    {
        newAbilityText = GameObject.Find("NewAbilityController").GetComponent<NewAbilityTextScript>();
        player = GameObject.Find("Player").GetComponent<PlayerControll>();
    }

    // Update is called once per frame
    void Update()
    {
        if (trigger && !manaRegenChestOpened) // Om Spelaren är nära kistan och om han inte har tagit den förr
        {
            if (Input.GetKey(KeyCode.E))
            {
                animator.SetTrigger("Open");
                animator.SetBool("IsOpened", true);
                PlayerInfo.unlockedManaRegen = true; // Spelaren upplåser mana regen
                player.OpenChestCutScene();
                newAbilityText.index = 5;
                manaRegenChestOpened = true;
            }

        }

        if (manaRegenChestOpened)// Om spelaren har öppnat kistan förr
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

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player")) // Spelaren är inte nära kistan
        {
            trigger = false;
        }
    }
}
