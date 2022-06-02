using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurrencyOneChest : MonoBehaviour
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
        if (trigger & !currencyChestOneOpened)
        {
            if (Input.GetKey(KeyCode.E))
            {
                animator.SetTrigger("Open");
                animator.SetBool("IsOpened", true);
                currency.IncreaseCurrency(300);
                currencyChestOneOpened = true;
            }

        }

        if (currencyChestOneOpened)
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
