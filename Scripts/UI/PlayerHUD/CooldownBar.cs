﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CooldownBar : MonoBehaviour
{
    public Slider slider;
    public Image fill;

    public void SetCooldown(float cooldown)
    {
        slider.value = cooldown * -1;
    }

    public void SetMaxCooldown(float cooldown)
    {
        //slider.maxValue = cooldown;
        slider.minValue = cooldown * -1;
        slider.value = cooldown * -1;
    }
}
