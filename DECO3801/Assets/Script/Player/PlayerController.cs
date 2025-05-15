using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;

/// <summary>
/// Controls the player�s movement between fixed vertical lanes and shooting projectiles.
/// </summary>
public class PlayerController : MonoBehaviour
{
    [SerializeField] private float speed;
    private Rigidbody2D rb;

    [Header("Gun")]
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private Transform firePoint;
    [SerializeField] private Slider reloadSlider;
    [SerializeField] private float reloadTime = 0.5f;
    private float timer = 0f;
    private bool canFire = true;

    private float[] positionsY = { -3f, -1f, 1f, 3f, 5f }; // Allowed Y positions
    private int currentPositionIndex = 1; // Start at positionY[1]
    private float moveDuration = 0.1f; // Duration of movement
    bool isMoving = false;
    private int heldDirection = 0; // 1 for W, -1 for S, 0 for none

    /// <summary>
    /// Initializes the Rigidbody2D component.
    /// </summary>
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        reloadSlider.value = timer;
        reloadSlider.minValue = 0;
        reloadSlider.maxValue = reloadTime;
    }

    /// <summary>
    /// Updates the player each frame by handling movement and shooting.
    /// </summary>
    private void Update()
    {

        HandleMovement();
        HandleShooting();
    }

    /// <summary>
    /// Handles vertical movement using W and S keys.
    /// </summary>
    private void HandleMovement()
    {
        if (Input.GetKey(KeyCode.W))
            heldDirection = 1;
        else if (Input.GetKey(KeyCode.S))
            heldDirection = -1;
        else
            heldDirection = 0;

        if (!isMoving && heldDirection != 0)
        {
            StartCoroutine(MovePlayer(heldDirection));
        }
    }
    /// <summary>
    /// Smoothly moves the player up or down between predefined Y positions.
    /// </summary>
    /// <param name="direction">1 for up, -1 for down.</param>
    private IEnumerator MovePlayer(int direction)
    {
        isMoving = true;

        int newPositionIndex = Mathf.Clamp(currentPositionIndex + direction, 0, positionsY.Length - 1);

        if (newPositionIndex == currentPositionIndex) // Can't move further
        {
            isMoving = false;
            yield break;
        }

        currentPositionIndex = newPositionIndex;
        float startY = transform.position.y;
        float targetY = positionsY[currentPositionIndex];
        float elapsedTime = 0f;

        while (elapsedTime < moveDuration)
        {
            float newY = Mathf.Lerp(startY, targetY, elapsedTime / moveDuration);
            transform.position = new Vector3(transform.position.x, newY, transform.position.z);

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Snap to final Y position
        transform.position = new Vector3(transform.position.x, targetY, transform.position.z);
        isMoving = false;

        // Auto-move again if still holding key and another move is possible
        if (heldDirection == 1 && Input.GetKey(KeyCode.W) && currentPositionIndex < positionsY.Length - 1)
        {
            StartCoroutine(MovePlayer(1));
        }
        else if (heldDirection == -1 && Input.GetKey(KeyCode.S) && currentPositionIndex > 0)
        {
            StartCoroutine(MovePlayer(-1));
        }
    }

    /// <summary>
    /// Handles input and reload logic for shooting projectiles.
    /// </summary>
    private void HandleShooting()
    {
        timer += Time.deltaTime;
        reloadSlider.value = timer;
        if (timer > reloadTime)
        {
            canFire = true;
        }

        if (Input.GetKeyDown(KeyCode.Space) && canFire)
        {
            canFire = false;
            FireProjectile();
            timer = 0f;
        }
    }

    /// <summary>
    /// Instantiates a projectile at the fire point.
    /// </summary>
    private void FireProjectile()
    {
        GameObject projectile = Instantiate(projectilePrefab, firePoint.position, Quaternion.identity);
    } 
}
