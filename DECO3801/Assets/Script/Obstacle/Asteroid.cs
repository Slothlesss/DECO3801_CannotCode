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
            effect.Play();
            GetComponent<Collider2D>().enabled = false;
            transform.GetChild(0).gameObject.SetActive(false);
            Destroy(gameObject, 1f);
        }
    }
}
