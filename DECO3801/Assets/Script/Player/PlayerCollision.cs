using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerCollision : MonoBehaviour
{
    [SerializeField] private GameObject heartPool;
    [SerializeField] private UIHeart[] hearts;
    [SerializeField] private GameObject gameOverPanel;
    [SerializeField] private float dmgTakenInterval = 1.5f;
    private int health;
    private bool canLoseHealth = true;

    private void Start()
    {
        health = 3;
        hearts = heartPool.GetComponentsInChildren<UIHeart>();
        gameOverPanel.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Coin")) {
            Destroy(other.gameObject);
            // Update UI Score
            PlayerStats.Instance.Score++;
        }
        if (other.CompareTag("Enemy") && canLoseHealth)
        {
            LoseHealth();
        }
    }

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
            gameOver();
        }
        Debug.Log("Lose Heath");

        health -= 1;
        canLoseHealth = false;
        StartCoroutine(HealthCooldown(dmgTakenInterval));
    }
    private IEnumerator HealthCooldown(float delay)
    {
        yield return new WaitForSeconds(delay);
        canLoseHealth = true;
    }

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

    private void gameOver()
    {
        gameOverPanel.SetActive(true);
        Time.timeScale = 0f;
    }
}
