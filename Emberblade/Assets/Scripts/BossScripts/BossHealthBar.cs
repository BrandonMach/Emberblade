using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossHealthBar : MonoBehaviour //Detta �r skrivet av: Sebastian
{
    // Start is called before the first frame update
    public Slider healthBar;
    public Color low;
    public Color high;
    public Vector3 offset;
    public void SetHealth(float health, float maxHealth) // S�tter Boss Health bar 
    {
        healthBar.gameObject.SetActive(true); // S�tter p� health baren
        healthBar.value = health;
        healthBar.maxValue = maxHealth;

        healthBar.fillRect.GetComponentInChildren<Image>().color = Color.Lerp(low, high, healthBar.normalizedValue); // fyller p� Bossens h�lsa
    }
}
