using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GradientBar : MonoBehaviour
{
    private Slider slider;
    [SerializeField] private Gradient gradient;
    [SerializeField] Image fill;

    private void Start()
    {
        slider = GetComponent<Slider>();
        slider.maxValue = 3;
        slider.minValue = 0;
        slider.value = 0;
        fill.color = gradient.Evaluate(0f);
    }

    public void SetValue(int value)
    {
        slider.value = value;
        fill.color = gradient.Evaluate(slider.normalizedValue);
    }

    public int GetMaxValue()
    {
        return (int)slider.maxValue;
    }
    public int GetMinValue()
    {
        return (int)slider.minValue;
    }
}
