using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EmotionCycler : MonoBehaviour
{
    [SerializeField] private Image[] frustrationImages;
    [SerializeField] private Image[] focusImages;
    [SerializeField] private Image[] fatigueImages;

    private int frustrationIndex = 0;
    private int focusIndex = 0;
    private int fatigueIndex = 0;

    private void Start()
    {
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

    // === Frustration ===
    public void FrustrationCycleUp()
    {
        frustrationIndex = (frustrationIndex + 1) % frustrationImages.Length;
        SetEmotionColor(frustrationImages, frustrationIndex);
    }

    public void FrustrationCycleDown()
    {
        frustrationIndex = (frustrationIndex - 1 + frustrationImages.Length) % frustrationImages.Length;
        SetEmotionColor(frustrationImages, frustrationIndex);
    }

    // === Focus ===
    public void FocusCycleUp()
    {
        focusIndex = (focusIndex + 1) % focusImages.Length;
        SetEmotionColor(focusImages, focusIndex);
    }

    public void FocusCycleDown()
    {
        focusIndex = (focusIndex - 1 + focusImages.Length) % focusImages.Length;
        SetEmotionColor(focusImages, focusIndex);
    }

    // === Fatigue ===
    public void FatigueCycleUp()
    {
        fatigueIndex = (fatigueIndex + 1) % fatigueImages.Length;
        SetEmotionColor(fatigueImages, fatigueIndex);
    }

    public void FatigueCycleDown()
    {
        fatigueIndex = (fatigueIndex - 1 + fatigueImages.Length) % fatigueImages.Length;
        SetEmotionColor(fatigueImages, fatigueIndex);
    }
}
