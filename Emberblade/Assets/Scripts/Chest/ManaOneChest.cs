using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManaOneChest : MonoBehaviour //Detta är skrivet av: Sebastian
{
    [SerializeField] Animator animator;

    [SerializeField] bool trigger;
    public static bool manaChestOneOpened;
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
        if (trigger && !manaChestOneOpened) // Om Spelaren är nära kistan och om han inte har tagit den förr
        {
            if (Input.GetKey(KeyCode.E))
            {
                animator.SetTrigger("Open");
                animator.SetBool("IsOpened", true);
                PlayerInfo.maxEnergy += 15; // Spelaren får mer max mana
                player.OpenChestCutScene();
                newAbilityText.index = 4;
                manaChestOneOpened = true;
            }

        }

        if (manaChestOneOpened) // Om spelaren har öppnat kistan förr
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
