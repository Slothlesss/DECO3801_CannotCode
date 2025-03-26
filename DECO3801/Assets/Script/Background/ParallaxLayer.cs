using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxLayer : MonoBehaviour
{
    private float startPos;
    private float length;
    [SerializeField] private GameObject cam;
    [SerializeField] private float parallaxFactor;

    private void Start()
    {
        startPos = transform.position.x;
        length = GetComponent<SpriteRenderer>().bounds.size.x;
    }

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
