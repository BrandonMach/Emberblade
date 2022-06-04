using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthOneChest : MonoBehaviour //Detta är skrivet av: Sebastian
{
    [SerializeField] Animator animator;

    [SerializeField] bool trigger;
    public static bool healthChestOneOpened;
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
        if (trigger && !healthChestOneOpened)
        {
            if (Input.GetKey(KeyCode.E))
            {
                animator.SetTrigger("Open");
                animator.SetBool("IsOpened", true);
                PlayerInfo.maxHealth += 15;
                player.OpenChestCutScene();
                newAbilityText.index = 3;
                healthChestOneOpened = true;
            }

        }

        if (healthChestOneOpened)
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
