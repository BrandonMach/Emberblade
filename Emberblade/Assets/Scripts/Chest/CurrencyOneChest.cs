using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurrencyOneChest : MonoBehaviour //Detta �r skrivet av: Sebastian
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
        if (trigger && !currencyChestOneOpened)// Om Spelaren �r n�ra kistan och om han inte har tagit den f�rr
        {
            if (Input.GetKey(KeyCode.E))
            {
                animator.SetTrigger("Open");
                animator.SetBool("IsOpened", true);
                currency.IncreaseCurrency(300);// Spelaren f�r 300 in-game Currency
                currencyChestOneOpened = true;
            }

        }

        if (currencyChestOneOpened)// Om spelaren har �ppnat kistan f�rr
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

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))// Spelaren �r inte n�ra kistan
        {
            trigger = false;
        }
    }
}
