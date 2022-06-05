using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnergyBar : MonoBehaviour //Detta är skrivet av: Philip
{

    [SerializeField] Slider slider;
    public void SetMaxEnergy(int energy) //Gör så att slider value får en bestämd max energi.
    {
        slider.maxValue = energy;
    }

    public void currentEnergy(int energy) //Gör så att slider value får ett energi.
    {
        slider.value = energy;
    }

    public void SetEnergy(int energy) //Gör så att slider value visar det satta energin.
    {
        slider.value = energy;
    }
}
