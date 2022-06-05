using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnergyBar : MonoBehaviour //Detta �r skrivet av: Philip
{

    [SerializeField] Slider slider;
    public void SetMaxEnergy(int energy)
    {
        slider.maxValue = energy;
    }

    public void currentEnergy(int energy)
    {
        slider.value = energy;
    }

    public void SetEnergy(int energy)
    {
        slider.value = energy;
    }
}
