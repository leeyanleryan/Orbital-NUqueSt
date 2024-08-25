using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnergyBar : MonoBehaviour
{
    public Slider slider;
    public void SetMaxHealth(float energy)
    {
        slider.maxValue = energy;
    }

    public void SetHealth(float energy)
    {
        slider.value = energy;
    }
}
