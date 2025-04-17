using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Collectable : MonoBehaviour
{
    [SerializeField] protected float speed = 10f;
    [SerializeField] protected float lifetime = 5f;
    [SerializeField] protected float rotationSpeed = 100f;
    private Rigidbody2D rb;
    /// <summary>
    /// Initializes the collectable with a movement direction and sets its velocity.
    /// Also schedules its destruction after a set lifetime.
    /// </summary>
    /// <param name="dir">The direction the obstacle should move in.</param>
    public virtual void Initialize(Vector2 dir)
    {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = dir * speed;
        Destroy(gameObject, lifetime); // Destroy after lifetime
    }

    protected virtual void OnTriggerEnter2D(Collider2D other)
    {
        //base effect here
    } 
}
