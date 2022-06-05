using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManaRegenChest : MonoBehaviour //Detta �r skrivet av: Sebastian
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
        if (trigger && !manaRegenChestOpened) // Om Spelaren �r n�ra kistan och om han inte har tagit den f�rr
        {
            if (Input.GetKey(KeyCode.E))
            {
                animator.SetTrigger("Open");
                animator.SetBool("IsOpened", true);
                PlayerInfo.unlockedManaRegen = true; // Spelaren uppl�ser mana regen
                player.OpenChestCutScene();
                newAbilityText.index = 5;
                manaRegenChestOpened = true;
            }

        }

        if (manaRegenChestOpened)// Om spelaren har �ppnat kistan f�rr
        {
            animator.SetBool("IsOpened", true);
        }

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player")) // Spelaren �r n�ra kistan
        {
            trigger = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player")) // Spelaren �r inte n�ra kistan
        {
            trigger = false;
        }
    }
}
