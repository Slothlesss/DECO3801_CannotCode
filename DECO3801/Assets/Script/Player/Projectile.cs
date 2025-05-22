using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Controls the behavior of a projectile shot by the player,
/// including movement, lifetime, and collision with enemies.
/// </summary>
public class Projectile : MonoBehaviour
{
    [SerializeField] private float speed = 10f;
    [SerializeField] private float lifetime = 5f;
    private Rigidbody2D rb;

    /// <summary>
    /// Initializes the projectile’s movement and schedules it for destruction after its lifetime.
    /// </summary>
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = Vector2.right * speed;
        Destroy(gameObject, lifetime); // Destroy after lifetime
    }

    /// <summary>
    /// Handles collision with objects tagged as "Enemy".
    /// Destroys both the enemy and the projectile upon impact.
    /// </summary>
    /// <param name="other">The collider the projectile has hit.</param>
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy") || other.CompareTag("AsteroidBelt"))
        {
            Destroy(gameObject);
        } 
    }
}
