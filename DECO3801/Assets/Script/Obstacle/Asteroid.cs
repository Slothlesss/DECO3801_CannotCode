using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Update
/// </summary>
public class Asteroid : Obstacle
{
    protected override void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Bullet"))
        {
            if (effect)
            {
                effect.Play();
                gameObject.SetActive(false);
                Destroy(transform.parent.gameObject, 1f);
            }
        }
    }
}
