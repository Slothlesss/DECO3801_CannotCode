using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Planet : Obstacle
{
    [SerializeField] private Sprite[] sprites;
    public override void Initialize(Vector2 dir)
    {
        base.Initialize(dir);
        int ranNum = Random.Range(0, 2);
        GetComponentInChildren<SpriteRenderer>().sprite = sprites[ranNum];
        if (ranNum == 1)
        {
            GetComponentInChildren<SpriteRenderer>().color = new Color(0.5f, 1f, 0.976f);
            var main = GetComponentInChildren<ParticleSystem>().main;
            main.startColor = new ParticleSystem.MinMaxGradient(Color.gray, Color.blue);
        }
    }
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
