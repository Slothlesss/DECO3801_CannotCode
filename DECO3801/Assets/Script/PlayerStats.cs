using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerStats : Singleton<PlayerStats>
{
    [SerializeField] private TextMeshProUGUI scoreUI;
    private int score;
    public int Score
    {
        get
        {
            return this.score;
        }
        set
        {
            this.score = value;
            scoreUI.text = value.ToString();
        }

    }
    private void Start()
    {
        Score = 0;
    }
}
