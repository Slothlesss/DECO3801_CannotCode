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

}
