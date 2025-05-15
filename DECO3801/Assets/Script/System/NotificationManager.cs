using UnityEngine;
using TMPro;

/// <summary>
/// Manages on-screen notifications for different emotional and cognitive states (e.g., frustration, focus, fatigue).
/// </summary>
public class NotificationManager : MonoBehaviour
{
    [SerializeField] private GameObject firstNotification;
    [SerializeField] private GameObject secondNotification;
    [SerializeField] private TextMeshProUGUI firstTitleUI;
    [SerializeField] private TextMeshProUGUI firstContentUI;
    [SerializeField] private TextMeshProUGUI secondTitleUI;
    [SerializeField] private TextMeshProUGUI secondContentUI;
    private GameObject currentPopUp;
    private EmotionCycler emotionCycler;

    /// <summary>
    /// Initializes the notification system by hiding all pop-ups.
    /// </summary>
    private void Start() {
        PopUp("FirstNotification", false);
        PopUp("SecondNotification", false);

    }

    /// <summary>
    /// Shows or hides a notification based on its name.
    /// </summary>
    /// <param name="name">"FirstNotification" or "SecondNotification".</param>
    /// <param name="isActive">True to show, false to hide.</param>
    public void PopUp(string name, bool isActive)
    {
        switch (name)
        {
            case "FirstNotification":
                firstNotification.SetActive(isActive);
                break;
            case "SecondNotification":
                secondNotification.SetActive(isActive);
                break;
        }
    }

    /// <summary>
    /// Toggles the visibility of the first notification (for testing).
    /// </summary>
    public void PopFirst() {
        if (firstTitleUI.IsActive())
        {
            firstNotification.SetActive(false);
        }
        else
        {
            firstNotification.SetActive(true);
        }
    }

    /// <summary>
    /// Toggles the visibility of the second notification (for testing).
    /// </summary>
    public void PopSecond() {
        if (secondTitleUI.IsActive())
        {
            secondNotification.SetActive(false);
        }
        else
        {
            secondNotification.SetActive(true);
        }
    }

    /// <summary>
    /// Updates the first notification’s title and content.
    /// </summary>
    public void UpdateFirstNotification(string title, string content) {
        firstTitleUI.text = title;
        firstContentUI.text = content;
    }

    /// <summary>
    /// Updates the second notification’s title and content.
    /// </summary>
    public void UpdateSecondNotification(string title, string content) {
        secondTitleUI.text = title;
        secondContentUI.text = content;
    }

    /// <summary>
    /// Displays a notification in either the first or second panel.
    /// </summary>
    /// <param name="title">The title of the notification.</param>
    /// <param name="content">The content of the notification.</param>
    /// <param name="useFirstNotification">If true, shows in the first notification UI; otherwise, second.</param>
    private void ShowNotification(string title, string content, bool useFirstNotification)
    {
        if (useFirstNotification)
        {
            UpdateFirstNotification(title, content);
            PopUp("FirstNotification", true);
        }
        else
        {
            UpdateSecondNotification(title, content);
            PopUp("SecondNotification", true);
        }
    }

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
        ShowNotification(title, content, true);
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
        ShowNotification(title, content, false);
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
        ShowNotification(title, content, false);
    }
}
