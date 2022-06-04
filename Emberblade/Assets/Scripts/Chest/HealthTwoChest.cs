using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthTwoChest : MonoBehaviour
{
    [SerializeField] Animator animator;

    [SerializeField] bool trigger;
    public static bool healthChestTwoOpened;
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
        if (trigger && !healthChestTwoOpened)
        {
            if (Input.GetKey(KeyCode.E))
            {
                animator.SetTrigger("Open");
                animator.SetBool("IsOpened", true);
                PlayerInfo.maxHealth += 15;
                player.OpenChestCutScene();
                newAbilityText.index = 3;
                healthChestTwoOpened = true;
            }

        }

        if (healthChestTwoOpened)
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
