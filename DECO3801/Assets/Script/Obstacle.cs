using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    [SerializeField] private float speed = 10f;
    [SerializeField] private float lifetime = 5f;
    [SerializeField] private float rotationSpeed = 100f;
    private Rigidbody2D rb;
    public void Initialize(Vector2 dir)
    {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = dir * speed;
        Destroy(gameObject, lifetime); // Destroy after lifetime
    }

    private void Update()
    {
        transform.Rotate(0, 0, rotationSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            Destroy(other.gameObject);
            Destroy(gameObject);
        }
    }
}
