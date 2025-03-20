using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private float speed = 5f; // Speed of the camera movement

    private void FixedUpdate()
    {
        transform.position += Vector3.right * speed * Time.fixedDeltaTime;
    }

}
