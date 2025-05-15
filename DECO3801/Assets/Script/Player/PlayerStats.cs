using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerStats : Singleton<PlayerStats>
{
    /// <summary>
    /// Int for number of coins collected by the player.
    /// </summary>
    private int coins;
    /// <summary>
    /// UI text for coins collected.
    /// </summary>
    [SerializeField] private TextMeshProUGUI coinsUI;

    /// <summary>
    /// Int for player score based on distance travelled.
    /// </summary>
    private int score;
    /// <summary>
    /// UI text for score.
    /// </summary>
    [SerializeField] private TextMeshProUGUI scoreUI;
    /// <summary>
    /// Float storage of score for casting to an int.
    /// </summary>
    private float scoreFloat;

    public int Coins
    {
        get
        {
            return this.coins;
        }
        set
        {
            this.coins = value;
            coinsUI.text = value.ToString();
        }

    }

    public int Score
    {
        get
        {
            return this.score;
        }
        set
        {
            this.score = value;
            scoreUI.text = "Score: " + value.ToString();
        }

    }
    private void Start()
    {
        Time.timeScale = 0;
        Coins = 0;
        Score = 0;
    }

    /// <summary>
    /// Runs when game is active, stops when game is over.
    /// </summary>
    private void Update()
    {
        if(GameManager.Instance.gameState == GameState.Running) {
            UpdateScore();
        }
    }


    private void UpdateScore()
    {
        PlayerCollision playerCollision = FindObjectOfType<PlayerCollision>();
        if (playerCollision != null && playerCollision.health != 0)
        {
            scoreFloat += 0.1f;
            Score = Mathf.RoundToInt(scoreFloat);
        }
        else if (playerCollision != null && playerCollision.health == 0)
        {
            Score = Mathf.RoundToInt(scoreFloat);
        }
    }
}
