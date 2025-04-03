using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour
{
    [SerializeField] private GameObject heart1;
    [SerializeField] private GameObject heart2;
    [SerializeField] private GameObject heart3;
    [SerializeField] private GameObject gameOverPanel;
    private int health;
    private float lostHealth = 3f;
    private float timer = 0f;
    private bool canLoseHealth = true;

    private void Start()
    {
        health = 3;
        gameOverPanel.SetActive(false);
    }

    private void Update()
    {
        HandleCollisions();
    }

    private void HandleCollisions()
    {
        // Check if 3 seconds has past since the last time player collided with an obstacle.
        timer += Time.deltaTime;
        if (timer > lostHealth)
        {
            canLoseHealth = true;
            timer = 0f;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // If player collides with an enemy object, player loses health.
        if (other.CompareTag("Enemy"))
        {
            if (canLoseHealth)
            {
                if (health == 3)
                {
                    heart3.SetActive(false);
                }
                else if (health == 2)
                {
                    heart2.SetActive(false);
                }
                else if (health == 1)
                {
                    // If player has last all 3 hearts, game is over.
                    heart1.SetActive(false);
                    gameOver();
                }

                health -= 1;
                canLoseHealth = false;
            }
        }
    }

    private void gameOver()
    {
        gameOverPanel.SetActive(true);
        Time.timeScale = 0f;
    }
}
