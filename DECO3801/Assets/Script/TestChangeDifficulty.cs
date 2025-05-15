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
        hardToggle.isOn = true;
        easyToggle.onValueChanged.AddListener(isOn =>
        {
            if (isOn) GameManager.Instance.SetDifficulty(Difficulty.Easy);
        });

        mediumToggle.onValueChanged.AddListener(isOn =>
        {
            if (isOn) GameManager.Instance.SetDifficulty(Difficulty.Medium);
        });

        hardToggle.onValueChanged.AddListener(isOn =>
        {
            if (isOn) GameManager.Instance.SetDifficulty(Difficulty.Hard);
        });
    }
}