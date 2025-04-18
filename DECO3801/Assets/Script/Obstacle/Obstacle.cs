using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Represents an obstacle in the game that moves, rotates, and interacts with the player.
/// </summary>
public abstract class Obstacle : MonoBehaviour
{
    [SerializeField] protected float speed = 10f;
    [SerializeField] protected float lifetime = 5f;
    [SerializeField] protected float rotationSpeed = 100f;
    private Rigidbody2D rb;

    [SerializeField] protected ParticleSystem effect;

    /// <summary>
    /// Initializes the obstacle with a movement direction and sets its velocity.
    /// Also schedules its destruction after a set lifetime.
    /// </summary>
    /// <param name="dir">The direction the obstacle should move in.</param>
    public virtual void Initialize(Vector2 dir)
    {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = dir * speed;
        Destroy(gameObject, lifetime); // Destroy after lifetime
    }

    /// <summary>
    /// Rotates the obstacle continuously every frame.
    /// </summary>
    protected virtual void Update()
    {
        transform.Rotate(0, 0, rotationSpeed * Time.deltaTime);
    }

    /// <summary>
    /// Handles collision with the player.
    /// Destroys both the player and the obstacle upon contact.
    /// </summary>
    /// <param name="other">The collider that entered the trigger.</param>
    protected virtual void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Destroy(other.gameObject);
            Destroy(gameObject);
        }
    }
}
