using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// A projectile that travels forward and pierces through enemies.
/// It does not get destroyed on impact and can damage multiple enemies.
/// </summary>
public class ChargedProjectile : MonoBehaviour
{
    [SerializeField] private float speed = 15f;         // Speed of the charged projectile
    [SerializeField] private float lifetime = 5f;       // Lifetime before self-destruction
    [SerializeField] private int damage = 2;            // Optional: how much damage to apply

    private Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = Vector2.right * speed;
        Destroy(gameObject, lifetime); // Auto-destroy after time
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            // Deal damage if enemy has a health component
            // DO NOT destroy this projectile on hit
        }
    }
}