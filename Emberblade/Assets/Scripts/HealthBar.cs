using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour //Detta �r skrivet av: Philip
{

    [SerializeField] Slider slider;
    public void SetMaxHealth(int health) //G�r s� att slider value f�r en best�md max liv.
    {
        slider.maxValue = health;
    }

    public void currentHealth(int health) //G�r s� att slider value f�r ett liv.
    {
        slider.value = health;
    }

    public void SetHealth(int health) //G�r s� att slider value visar det satta livet.
    {
        slider.value = health;
    }
}
