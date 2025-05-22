using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidBelt : Obstacle
{
    protected override void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("ChargedBullet"))
        {
            effect.Play();
            GetComponent<Collider2D>().enabled = false;
            transform.GetChild(0).gameObject.SetActive(false);
            Destroy(gameObject, 1f);
        }
    }
}
