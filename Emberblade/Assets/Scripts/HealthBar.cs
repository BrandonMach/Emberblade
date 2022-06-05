using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour //Detta är skrivet av: Philip
{

    [SerializeField] Slider slider;
    public void SetMaxHealth(int health) //Gör så att slider value får en bestämd max liv.
    {
        slider.maxValue = health;
    }

    public void currentHealth(int health) //Gör så att slider value får ett liv.
    {
        slider.value = health;
    }

    public void SetHealth(int health) //Gör så att slider value visar det satta livet.
    {
        slider.value = health;
    }
}
