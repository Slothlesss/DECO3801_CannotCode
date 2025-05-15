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
        switch (GameManager.Instance.difficulty)
        {
            case Difficulty.Easy:
                easyToggle.isOn = true;
                break;
            case Difficulty.Medium:
                mediumToggle.isOn = true;
                break;
            case Difficulty.Hard:
                hardToggle.isOn = true;
                break;
        }
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