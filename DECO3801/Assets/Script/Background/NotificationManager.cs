using UnityEngine;
using TMPro;

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
    private void Start() {
        PopUp("FirstNotification", false);
        PopUp("SecondNotification", false);

    }
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
    
    // Testing method.
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

    // Update first pop-up notification.
    public void UpdateFirstNotification(string title, string content) {
        firstTitleUI.text = title;
        firstContentUI.text = content;
    }

    // Update second pop-up notification.
    public void UpdateSecondNotification(string title, string content) {
        secondTitleUI.text = title;
        secondContentUI.text = content;
    }

    // Show notification based on the current state of notifications.
    // If first is active, update second. If second is active, update first.
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
