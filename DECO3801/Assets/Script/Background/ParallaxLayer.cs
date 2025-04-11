using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ParallaxLayer moves the background layer at a different speed than the camera to create
/// a parallax scrolling effect in a 2D game.
/// </summary>
public class ParallaxLayer : MonoBehaviour
{
    private float startPos;
    private float length;
    [SerializeField] private GameObject cam;
    [SerializeField] private float parallaxFactor;

    // <summary>
    /// Initializes the start position and calculates the length of the sprite for wrapping.
    /// </summary>
    private void Start()
    {
        startPos = transform.position.x;
        length = GetComponent<SpriteRenderer>().bounds.size.x;
    }

    /// <summary>
    /// Updates the position of the background layer to simulate parallax.
    /// </summary>
    private void FixedUpdate()
    {
        float distance = cam.transform.position.x * parallaxFactor; //how much distance a background should shift
        float movement = cam.transform.position.x * (1 - parallaxFactor); // how far the camera has moved

        transform.position = new Vector3(startPos + distance, transform.position.y, transform.position.z);

        if (movement > startPos + length)
        {
            startPos += length; //Shift the background forward
        }
    }
}
