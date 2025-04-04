using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EmotionCycler : MonoBehaviour
{
    [SerializeField] private Image[] frustrationImages;
    [SerializeField] private Image[] focusImages;
    [SerializeField] private Image[] fatigueImages;
    [SerializeField] private NotificationManager notificationManager;
    private int frustrationIndex = 0;
    private int focusIndex = 0;
    private int fatigueIndex = 0;

    private void Start()
    {
        notificationManager = FindObjectOfType<NotificationManager>();
        SetEmotionColor(frustrationImages, frustrationIndex);
        SetEmotionColor(focusImages, focusIndex);
        SetEmotionColor(fatigueImages, fatigueIndex);
    }

    // Utility method to set one white and rest grey
    private void SetEmotionColor(Image[] images, int activeIndex)
    {
        for (int i = 0; i < images.Length; i++)
        {
            images[i].color = (i == activeIndex) ? Color.white : Color.grey;
        }
    }

    // Get emotion indexes.
    public int GetFrustrationIndex()
    {
        return frustrationIndex;
    }

    public int GetFocusIndex()
    {
        return focusIndex;
    }

    public int GetFatigueIndex()
    {
        return fatigueIndex;
    }
    
    // === Frustration ===
    public void FrustrationCycleUp()
    {
        frustrationIndex = (frustrationIndex + 1) % frustrationImages.Length;
        SetEmotionColor(frustrationImages, frustrationIndex);
        notificationManager.FrustrationHandler(frustrationIndex);
    }

    public void FrustrationCycleDown()
    {
        frustrationIndex = (frustrationIndex - 1 + frustrationImages.Length) % frustrationImages.Length;
        SetEmotionColor(frustrationImages, frustrationIndex);
        notificationManager.FrustrationHandler(frustrationIndex);
    }

    // === Focus ===
    public void FocusCycleUp()
    {
        focusIndex = (focusIndex + 1) % focusImages.Length;
        SetEmotionColor(focusImages, focusIndex);
        notificationManager.FocusHandler(focusIndex);
    }

    public void FocusCycleDown()
    {
        focusIndex = (focusIndex - 1 + focusImages.Length) % focusImages.Length;
        SetEmotionColor(focusImages, focusIndex);
        notificationManager.FocusHandler(focusIndex);
    }

    // === Fatigue ===
    public void FatigueCycleUp()
    {
        fatigueIndex = (fatigueIndex + 1) % fatigueImages.Length;
        SetEmotionColor(fatigueImages, fatigueIndex);
        notificationManager.FatigueHandler(fatigueIndex);
    }

    public void FatigueCycleDown()
    {
        fatigueIndex = (fatigueIndex - 1 + fatigueImages.Length) % fatigueImages.Length;
        SetEmotionColor(fatigueImages, fatigueIndex);
        notificationManager.FatigueHandler(fatigueIndex);
    }
}
