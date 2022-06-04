using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour //Detta �r skrivet av: Philip
{

    [SerializeField] Slider slider;
    public void SetMaxHealth(int health)
    {
        slider.maxValue = health;
    }

    public void currentHealth(int health)
    {
        slider.value = health;
    }

    public void SetHealth(int health)
    {
        slider.value = health;
    }
}
