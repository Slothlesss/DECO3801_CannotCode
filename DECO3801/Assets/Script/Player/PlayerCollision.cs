using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Coin")) {
            Destroy(other.gameObject);
            // Update UI Score
            PlayerStats.Instance.Score++;
        }
    }
}
