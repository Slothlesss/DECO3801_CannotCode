using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Represents an obstacle in the game that moves, rotates, and interacts with the player.
/// </summary>
public class Obstacle : MonoBehaviour
{
    [SerializeField] private float speed = 10f;
    [SerializeField] private float lifetime = 5f;
    [SerializeField] private float rotationSpeed = 100f;
    private Rigidbody2D rb;

    /// <summary>
    /// Initializes the obstacle with a movement direction and sets its velocity.
    /// Also schedules its destruction after a set lifetime.
    /// </summary>
    /// <param name="dir">The direction the obstacle should move in.</param>
    public void Initialize(Vector2 dir)
    {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = dir * speed;
        Destroy(gameObject, lifetime); // Destroy after lifetime
    }

    /// <summary>
    /// Rotates the obstacle continuously every frame.
    /// </summary>
    private void Update()
    {
        transform.Rotate(0, 0, rotationSpeed * Time.deltaTime);
    }

    /// <summary>
    /// Handles collision with the player.
    /// Destroys both the player and the obstacle upon contact.
    /// </summary>
    /// <param name="other">The collider that entered the trigger.</param>
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Destroy(other.gameObject);
            Destroy(gameObject);
        }
    }
}
