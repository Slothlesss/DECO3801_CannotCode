using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class TestChangeDifficulty : MonoBehaviour
{
    public Toggle easyToggle;
    public Toggle mediumToggle;
    public Toggle hardToggle;

    void Start()
    {
        switch (GameManager.Instance.frustration)
        {
            case Frustration.Normal:
                easyToggle.isOn = true;
                break;
            case Frustration.Mild:
                mediumToggle.isOn = true;
                break;
            case Frustration.Moderate:
                hardToggle.isOn = true;
                break;
        }
        easyToggle.onValueChanged.AddListener(isOn =>
        {
            if (isOn) GameManager.Instance.SetFrustration(Frustration.Normal);
        });

        mediumToggle.onValueChanged.AddListener(isOn =>
        {
            if (isOn) GameManager.Instance.SetFrustration(Frustration.Mild);
        });

        hardToggle.onValueChanged.AddListener(isOn =>
        {
            if (isOn) GameManager.Instance.SetFrustration(Frustration.Moderate);
        });
    }
}