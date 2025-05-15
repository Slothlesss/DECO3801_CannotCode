using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Controls the behavior of a projectile shot by the player,
/// including movement, lifetime, and collision with enemies.
/// </summary>
public class Projectile2 : MonoBehaviour
{
    [SerializeField] private float speed = 10f;
    [SerializeField] private float lifetime = 5f;
    private Rigidbody2D rb;
    public GameObject hitEffectPrefab;

    /// <summary>
    /// Initializes the projectile�s movement and schedules it for destruction after its lifetime.
    /// </summary>
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = Vector2.left * speed;
        Destroy(gameObject, lifetime); // Destroy after lifetime
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Friendly"))
        {
            Instantiate(hitEffectPrefab, transform.position, Quaternion.identity);
            Destroy(gameObject); // Destroy the projectile
        }
    }
}
