using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurrencyOneChest : MonoBehaviour //Detta är skrivet av: Sebastian
{
    [SerializeField] Animator animator;

    [SerializeField] bool trigger;
    public static bool currencyChestOneOpened;
    Currency currency;

    void Start()
    {
        currency = GameObject.Find("Player").GetComponent<Currency>();
    }

    // Update is called once per frame
    void Update()
    {
        if (trigger && !currencyChestOneOpened)// Om Spelaren är nära kistan och om han inte har tagit den förr
        {
            if (Input.GetKey(KeyCode.E))
            {
                animator.SetTrigger("Open");
                animator.SetBool("IsOpened", true);
                currency.IncreaseCurrency(300);// Spelaren får 300 in-game Currency
                currencyChestOneOpened = true;
            }

        }

        if (currencyChestOneOpened)// Om spelaren har öppnat kistan förr
        {
            animator.SetBool("IsOpened", true);
        }

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))// Spelaren är nära kistan
        {
            trigger = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))// Spelaren är inte nära kistan
        {
            trigger = false;
        }
    }
}
