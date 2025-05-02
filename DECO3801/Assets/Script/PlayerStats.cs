using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class PlayerStats : Singleton<PlayerStats>
{
    private int coins;
    [SerializeField] private TextMeshProUGUI coinsUI;
    // [SerializeField] private TextMeshProUGUI gameOverCoinsUI;

    [SerializeField] private TextMeshProUGUI scoreUI;
    // [SerializeField] private TextMeshProUGUI gameOverScoreUI;
    private int score;
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
            // gameOverCoinsUI.text = "Coins:" + value.ToString();
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
            // gameOverScoreUI.text = "Score: " + value.ToString();
        }

    }

    
    private void Start()
    {
        Coins = 0;
        Score = 0;
    }

    private void Update()
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


    public void Replay() {
        // Score = 0;
        // Coins = 0;
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void Quit() {
        Application.Quit();
    }
}
