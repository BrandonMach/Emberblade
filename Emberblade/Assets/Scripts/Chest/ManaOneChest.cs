using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManaOneChest : MonoBehaviour
{
    [SerializeField] Animator animator;

    [SerializeField] bool trigger;
    public static bool manaChestOneOpened;
    PlayerInfo playerInfo;
    NewAbilityTextScript newAbilityText;
    PlayerControll player;

    void Start()
    {
        playerInfo = GameObject.Find("Player").GetComponent<PlayerInfo>();
        newAbilityText = GameObject.Find("NewAbilityController").GetComponent<NewAbilityTextScript>();
        player = GameObject.Find("Player").GetComponent<PlayerControll>();
    }

    // Update is called once per frame
    void Update()
    {
        if (trigger && !manaChestOneOpened)
        {
            if (Input.GetKey(KeyCode.E))
            {
                animator.SetTrigger("Open");
                animator.SetBool("IsOpened", true);
                playerInfo.maxEnergy += 15;
                player.PlayNewAbilityCutscene();
                newAbilityText.index = 4;
                manaChestOneOpened = true;
            }

        }

        if (manaChestOneOpened)
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
