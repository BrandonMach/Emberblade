using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnergyBar : MonoBehaviour //Detta �r skrivet av: Philip
{

    [SerializeField] Slider slider;
    public void SetMaxEnergy(int energy) //G�r s� att slider value f�r en best�md max energi.
    {
        slider.maxValue = energy;
    }

    public void currentEnergy(int energy) //G�r s� att slider value f�r ett energi.
    {
        slider.value = energy;
    }

    public void SetEnergy(int energy) //G�r s� att slider value visar det satta energin.
    {
        slider.value = energy;
    }
}
