using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealthbar : MonoBehaviour //Detta är skrivet av: Sebastian
{

    public Slider healthBar;
    public Color low;
    public Color high;
    public Vector3 offset;
    bool inCombat;

    float timer = 0;
    float maxTimer = 6;

    public void SetHealth(float health, float maxHealth)
    {
        healthBar.gameObject.SetActive(health < maxHealth);
        if (health < maxHealth)
        {
            inCombat = true;
        }
        healthBar.value = health;
        healthBar.maxValue = maxHealth;

        healthBar.fillRect.GetComponentInChildren<Image>().color = Color.Lerp(low, high, healthBar.normalizedValue);
    }


    // Update is called once per frame
    void Update()
    {
        healthBar.transform.position = Camera.main.WorldToScreenPoint(transform.parent.position + offset);

        if (inCombat)
        { 
            timer += Time.deltaTime;
            if (timer > maxTimer)
            {
                healthBar.gameObject.SetActive(false);
                inCombat = !inCombat;
                timer = 0f;
            }
        }
        Debug.Log(timer);
    }
}
