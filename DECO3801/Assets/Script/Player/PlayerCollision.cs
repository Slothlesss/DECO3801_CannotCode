using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

/// <summary>
/// Handles collision events for the player, including health loss, UI updates, and game over state.
/// </summary>
public class PlayerCollision : MonoBehaviour
{
    [SerializeField] private GameObject heartPool;
    [SerializeField] private UIHeart[] hearts;
    [SerializeField] private GameObject gameOverPanel;
    [SerializeField] private float dmgTakenInterval = 1.5f;
    public int health;
    private bool canLoseHealth = true;

    [SerializeField] private TextMeshProUGUI gameOverCoinsUI;

    [SerializeField] private TextMeshProUGUI gameOverScoreUI;

    private int coins;

    private int score;

    /// <summary>
    /// Initializes player health and UI at the start of the game.
    /// </summary>
    private void Start()
    {
        health = 3;
        hearts = heartPool.GetComponentsInChildren<UIHeart>();
        gameOverPanel.SetActive(false);
    }

    private IEnumerator CollectCoin(GameObject coin)
    {
        Animator anim = coin.GetComponent<Animator>();
        if (anim != null)
        {
            anim.SetTrigger("Collect");
            yield return new WaitForSeconds(0.4f); // match animation length
        }
        else
        {
        yield return new WaitForSeconds(0.2f); // fallback
        }

    }

    /// <summary>
    /// Triggered when the player collides with another collider.
    /// Handles coin collection and enemy collision.
    /// </summary>
    /// <param name="other">The collider the player has triggered.</param>
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Coin")) {
            PlayerStats.Instance.Score++;
            StartCoroutine(CollectCoin(other.gameObject));
            PlayerStats.Instance.Coins++;
        }
        if (other.CompareTag("Enemy") && canLoseHealth)
        {
            LoseHealth();
        }
    }

    /// <summary>
    /// Handles health deduction, UI update, and triggers game over if health reaches zero.
    /// </summary>
    private void LoseHealth()
    {
        StartCoroutine(HitCollision());
        if (health == 3)
        {
            hearts[2].GetComponent<Image>().color = Color.grey;
        }
        else if (health == 2)
        {
            hearts[1].GetComponent<Image>().color = Color.grey;
        }
        else if (health == 1)
        {
            // If player has last all 3 hearts, game is over.
            hearts[0].GetComponent<Image>().color = Color.grey;
            GameOver();
        }
        Debug.Log("Lose Heath");

        health -= 1;
        canLoseHealth = false;
        StartCoroutine(HealthCooldown(dmgTakenInterval));
    }

    // <summary>
    /// Waits for a specified time before allowing the player to take damage again.
    /// </summary>
    /// <param name="delay">Time to wait before resetting damage cooldown.</param>
    private IEnumerator HealthCooldown(float delay)
    {
        yield return new WaitForSeconds(delay);
        canLoseHealth = true;
    }

    /// <summary>
    /// Plays a hit animation by flashing the player sprite three times.
    /// </summary>
    private IEnumerator HitCollision()
    {
        SpriteRenderer playerSprite = GetComponent<SpriteRenderer>();
        for (int i = 0; i < 3; i++)
        {
            yield return new WaitForSeconds(0.1f);
            playerSprite.color = new Color(1, 1, 1, 0);
            yield return new WaitForSeconds(0.1f); 
            playerSprite.color = new Color(1, 1, 1, 1);
        }
    }

    /// <summary>
    /// Activates the game over screen and pauses the game.
    /// </summary>
    private void GameOver()
    {
        score = PlayerStats.Instance.Score;
        gameOverScoreUI.text = score.ToString();
        
        coins = PlayerStats.Instance.Coins;
        gameOverCoinsUI.text = coins.ToString();
        
        gameOverPanel.SetActive(true);
        Time.timeScale = 0f;       
    }
}
