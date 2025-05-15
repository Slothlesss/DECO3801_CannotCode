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

    [SerializeField] private NotificationManager notificationManager;
    private int frustrationIndex = 0;
    private int fatigueIndex = 0;
    private int focusIndex = 0;

    /// <summary>
    /// Initializes the emotion display and finds the NotificationManager instance.
    /// </summary>
    private void Start()
    {
        notificationManager = FindObjectOfType<NotificationManager>();
    }

    /// <summary>
    /// Gets the current Frustration index.
    /// </summary>
    public int GetFrustrationIndex()
    {
        return frustrationIndex;
    }

    /// <summary>
    /// Gets the current Focus index.
    /// </summary>
    public int GetFocusIndex()
    {
        return focusIndex;
    }

    /// <summary>
    /// Gets the current Fatigue index.
    /// </summary>
    public int GetFatigueIndex()
    {
        return fatigueIndex;
    }


    /// <summary>
    /// Increases the Frustration level and updates slider and notifications.
    /// </summary>
    public void FrustrationCycleUp()
    {
        frustrationIndex = (frustrationIndex + 1) % (frustrationSlider.GetMaxValue() + 1);
        frustrationSlider.SetValue(frustrationIndex);
        notificationManager.FrustrationHandler(frustrationIndex);
    }

    /// <summary>
    /// Decreases the Frustration level and updates slider and notifications.
    /// </summary>
    public void FrustrationCycleDown()
    {
        frustrationIndex = (frustrationIndex - 1) % (frustrationSlider.GetMaxValue() + 1);
        if (frustrationIndex <= 0) frustrationIndex = 0;
        frustrationSlider.SetValue(frustrationIndex);
        notificationManager.FrustrationHandler(frustrationIndex);
    }

    /// <summary>
    /// Increases the Focus level and updates slider and notifications.
    /// </summary>
    public void FatigueCycleUp()
    {
        fatigueIndex = (fatigueIndex + 1) % (fatigueSlider.GetMaxValue() + 1);
        fatigueSlider.SetValue(fatigueIndex);
        notificationManager.FatigueHandler(fatigueIndex);
    }

    /// <summary>
    /// Decreases the Focus level and updates slider and notifications.
    /// </summary>
    public void FatigueCycleDown()
    {
        fatigueIndex = (fatigueIndex - 1) % (fatigueSlider.GetMaxValue() + 1);
        if (fatigueIndex <= 0) fatigueIndex = 0;
        fatigueSlider.SetValue(fatigueIndex);
        notificationManager.FatigueHandler(fatigueIndex);
    }

    /// <summary>
    /// Increases the Fatigue level and updates slider and notifications.
    /// </summary>
    public void FocusCycleUp()
    {
        focusIndex = (focusIndex + 1) % (focusSlider.GetMaxValue() + 1);
        focusSlider.SetValue(focusIndex);
        notificationManager.FocusHandler(focusIndex);
    }

    /// <summary>
    /// Decreases the Fatigue level and updates slider and notifications.
    /// </summary>
    public void FocusCycleDown()
    {
        focusIndex = (focusIndex - 1) % (focusSlider.GetMaxValue() + 1);
        if (focusIndex <= 0) focusIndex = 0;
        focusSlider.SetValue(focusIndex);
        notificationManager.FocusHandler(focusIndex);
    }
}
