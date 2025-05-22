using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// EmotionCycler is responsible for cycling through different levels of three emotions:
/// Frustration, Focus, and Fatigue. It updates associated UI images and notifies the
/// NotificationManager of the current emotion state.
/// </summary>
public class EmotionCycler : MonoBehaviour
{
    [SerializeField] private GradientBar frustrationSlider;
    [SerializeField] private GradientBar fatigueSlider;
    [SerializeField] private GradientBar focusSlider;
    int frustration = 0;
    int fatigue = 0;
    int focus = 0;

    private void Update()
    {
        UpdateFrustration();
        UpdateFatigue();
        UpdateFocus();
    }

    public void UpdateFrustration()
    {
        frustration = (int)GameManager.Instance.frustration;
        frustrationSlider.SetValue(frustration);
        NotificationManager.Instance.FrustrationHandler(frustration);
    }

    public void UpdateFatigue()
    {
        fatigue = (int)GameManager.Instance.fatigue;
        fatigueSlider.SetValue(fatigue);
        NotificationManager.Instance.FatigueHandler(fatigue);
    }

    public void UpdateFocus()
    {
        focus = (int)GameManager.Instance.focus;
        focusSlider.SetValue(focus);
        NotificationManager.Instance.FocusHandler(focus);
    }
}
