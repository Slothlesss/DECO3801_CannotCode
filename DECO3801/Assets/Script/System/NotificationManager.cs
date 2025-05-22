using UnityEngine;
using TMPro;

/// <summary>
/// Manages on-screen notifications for different emotional and cognitive states (e.g., frustration, focus, fatigue).
/// </summary>
public class NotificationManager : Singleton<NotificationManager>
{
    [SerializeField] private GameObject firstNotification;
    [SerializeField] private GameObject secondNotification;
    [SerializeField] private GameObject thirdNotification;
    [SerializeField] private TextMeshProUGUI firstTitleUI;
    [SerializeField] private TextMeshProUGUI firstContentUI;
    [SerializeField] private TextMeshProUGUI secondTitleUI;
    [SerializeField] private TextMeshProUGUI secondContentUI;
    [SerializeField] private TextMeshProUGUI thirdTitleUI;
    [SerializeField] private TextMeshProUGUI thirdContentUI;

    /// <summary>
    /// Displays feedback based on the frustration level.
    /// </summary>
    /// <param name="frustrationIndex">Level from 0 (calm) to 3 (seriously frustrated).</param>
    public void FrustrationHandler(int frustrationIndex)
    {
        string title = "";
        string content = "";
        switch (frustrationIndex)
        {
            case 0: title = "You are calm."; content = "You are doing well!"; break;
            case 1: title = "You are slightly frustrated."; content = "Take a break if you need to."; break;
            case 2: title = "You are moderately frustrated."; content = "Consider taking a break."; break;
            case 3: title = "You are seriously frustrated."; content = "Take a break now!"; break;
        }
        firstNotification.SetActive(true);
        firstTitleUI.text = title;
        firstContentUI.text = content;

    }


    /// <summary>
    /// Displays feedback based on the fatigue level.
    /// </summary>
    /// <param name="fatigueIndex">Level from 0 (rested) to 3 (seriously fatigued).</param>
    public void FatigueHandler(int fatigueIndex)
    {
        string title = "";
        string content = "";
        switch (fatigueIndex)
        {
            case 0: title = "You are adequately rested."; content = "You are doing well!"; break;
            case 1: title = "You are slightly fatigued."; content = "Take a break if you need to."; break;
            case 2: title = "You are moderately fatigued."; content = "Consider taking a break."; break;
            case 3: title = "You are seriously fatigued."; content = "Take a break now!"; break;
        }
        secondNotification.SetActive(true);
        secondTitleUI.text = title;
        secondContentUI.text = content;
    }

    /// <summary>
    /// Displays feedback based on the focus level.
    /// </summary>
    /// <param name="focusIndex">Level from 0 (focused) to 3 (seriously distracted).</param>
    public void FocusHandler(int focusIndex)
    {
        string title = "";
        string content = "";
        switch (focusIndex)
        {
            case 0: title = "You are adequately focused."; content = "You are doing well!"; break;
            case 1: title = "You are slightly distracted."; content = "Try to refocus."; break;
            case 2: title = "You are moderately distracted."; content = "Consider taking a break."; break;
            case 3: title = "You are seriously distracted."; content = "Take a break now!"; break;
        }
        thirdNotification.SetActive(true);
        thirdTitleUI.text = title;
        thirdContentUI.text = content;
    }
}
