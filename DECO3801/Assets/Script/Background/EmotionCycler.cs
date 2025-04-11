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
    [SerializeField] private Image[] frustrationImages;
    [SerializeField] private Image[] focusImages;
    [SerializeField] private Image[] fatigueImages;
    [SerializeField] private NotificationManager notificationManager;
    private int frustrationIndex = 0;
    private int focusIndex = 0;
    private int fatigueIndex = 0;

    /// <summary>
    /// Initializes the emotion display and finds the NotificationManager instance.
    /// </summary>
    private void Start()
    {
        notificationManager = FindObjectOfType<NotificationManager>();
        SetEmotionColor(frustrationImages, frustrationIndex);
        SetEmotionColor(focusImages, focusIndex);
        SetEmotionColor(fatigueImages, fatigueIndex);
    }

    /// <summary>
    /// Highlights the active emotion level (white), greys out others.
    /// </summary>
    /// <param name="images">Array of emotion level images.</param>
    /// <param name="activeIndex">Index of the active emotion level.</param>
    private void SetEmotionColor(Image[] images, int activeIndex)
    {
        for (int i = 0; i < images.Length; i++)
        {
            images[i].color = (i == activeIndex) ? Color.white : Color.grey;
        }
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
    /// Increases the Frustration level and updates UI and notifications.
    /// </summary>
    public void FrustrationCycleUp()
    {
        frustrationIndex = (frustrationIndex + 1) % frustrationImages.Length;
        SetEmotionColor(frustrationImages, frustrationIndex);
        notificationManager.FrustrationHandler(frustrationIndex);
    }

    /// <summary>
    /// Decreases the Frustration level and updates UI and notifications.
    /// </summary>
    public void FrustrationCycleDown()
    {
        frustrationIndex = (frustrationIndex - 1 + frustrationImages.Length) % frustrationImages.Length;
        SetEmotionColor(frustrationImages, frustrationIndex);
        notificationManager.FrustrationHandler(frustrationIndex);
    }

    /// <summary>
    /// Increases the Focus level and updates UI and notifications.
    /// </summary>
    public void FocusCycleUp()
    {
        focusIndex = (focusIndex + 1) % focusImages.Length;
        SetEmotionColor(focusImages, focusIndex);
        notificationManager.FocusHandler(focusIndex);
    }

    /// <summary>
    /// Decreases the Focus level and updates UI and notifications.
    /// </summary>
    public void FocusCycleDown()
    {
        focusIndex = (focusIndex - 1 + focusImages.Length) % focusImages.Length;
        SetEmotionColor(focusImages, focusIndex);
        notificationManager.FocusHandler(focusIndex);
    }

    /// <summary>
    /// Increases the Fatigue level and updates UI and notifications.
    /// </summary>
    public void FatigueCycleUp()
    {
        fatigueIndex = (fatigueIndex + 1) % fatigueImages.Length;
        SetEmotionColor(fatigueImages, fatigueIndex);
        notificationManager.FatigueHandler(fatigueIndex);
    }

    /// <summary>
    /// Decreases the Fatigue level and updates UI and notifications.
    /// </summary>
    public void FatigueCycleDown()
    {
        fatigueIndex = (fatigueIndex - 1 + fatigueImages.Length) % fatigueImages.Length;
        SetEmotionColor(fatigueImages, fatigueIndex);
        notificationManager.FatigueHandler(fatigueIndex);
    }
}
