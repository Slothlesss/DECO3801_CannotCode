using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Update
/// </summary>
public class Asteroid : Obstacle
{
    [SerializeField] private Sprite[] sprites;
    public override void Initialize(Vector2 dir)
    {
        base.Initialize(dir);
        GetComponentInChildren<SpriteRenderer>().sprite = sprites[Random.Range(0, 2)];
    }
    protected override void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Bullet") || other.CompareTag("ChargedBullet"))
        {
            effect.Play();
            GetComponent<Collider2D>().enabled = false;
            transform.GetChild(0).gameObject.SetActive(false);
            Destroy(gameObject, 1f);
        }
    }
}
