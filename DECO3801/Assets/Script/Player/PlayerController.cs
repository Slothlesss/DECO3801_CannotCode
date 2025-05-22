using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;

/// <summary>
/// Controls the player�s movement between fixed vertical lanes and shooting projectiles.
/// </summary>
public class PlayerController : MonoBehaviour, IFocusListener
{
    [SerializeField] private float speed;
    private Rigidbody2D rb;

    [Header("Gun")]
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private Transform firePoint;
    [SerializeField] private Slider reloadSlider;
    [SerializeField] private float reloadTime = 3f;
    private float normalTimer = 0f;
    private bool normalShotAvailable = false;

    [Header("Charged Shot")]
    [SerializeField] private GameObject chargedProjectilePrefab;
    [SerializeField] private Slider chargeReloadSlider;
    [SerializeField] private float chargeTime = 1.0f; // Time to hold before charged shot
    [SerializeField] private float chargeReloadTime = 7.0f;
    private float chargeTimer = 0f;
    private float chargeReloadTimer = 0f;
    private bool chargedShotAvailable = false;
    private bool isChargeShotCharging = false;

    [Header("Movement")]
    private float[] positionsY = { -3f, -1f, 1f, 3f, 5f }; // Allowed Y positions
    private int currentPositionIndex = 2; // Start at positionY[2]
    private float moveDuration = 0.2f; // Duration of movement
    bool isMoving = false;
    private int heldDirection = 0; // 1 for W, -1 for S, 0 for none

    /// <summary>
    /// Initializes the Rigidbody2D component.
    /// </summary>
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        reloadSlider.value = normalTimer;
        reloadSlider.minValue = 0;
        reloadSlider.maxValue = reloadTime;
        chargeReloadSlider.maxValue = chargeReloadTime;

        GameManager.Instance.RegisterFocusListener(this);
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

    private void HandleShooting()
    {
        NormalShot();
        ChargeShot();
    }
    private void NormalShot()
    {
        if (!normalShotAvailable) return;

        normalTimer += Time.deltaTime;
        reloadSlider.value = normalTimer;
        if (normalTimer < reloadTime)
            return;

        if (Input.GetKeyDown(KeyCode.Space))
        {
            FireNormalProjectile();
            normalTimer = 0f;
        }

    }

    /// <summary>
    /// Handles input and reload logic for shooting projectiles.
    /// </summary>
    private void ChargeShot()
    {
        if (!chargedShotAvailable) return;

        chargeReloadTimer += Time.deltaTime;
        chargeReloadSlider.value = chargeReloadTimer;

        if (chargeReloadTimer < chargeReloadTime)
            return;

        // Start charging when Space is held
        if (Input.GetKeyDown(KeyCode.Space))
        {
            chargeTimer = 0f;
            chargeReloadSlider.value = 0f;
        }

        // Continue charging
        if (Input.GetKey(KeyCode.Space))
        {
            chargeTimer += Time.deltaTime;
        }

        // Fire based on duration when key is released
        if (Input.GetKeyUp(KeyCode.Space))
        {
            if (chargeTimer >= chargeTime)
            {
                FireChargedProjectile();
                chargeReloadTimer = 0f;
            }
            chargeTimer = 0f;
        }
    }

    /// <summary>
    /// Instantiates a standard projectile at the fire point.
    /// </summary>
    private void FireNormalProjectile()
    {
        GameObject projectile = Instantiate(projectilePrefab, firePoint.position, Quaternion.identity);
    }

    /// <summary>
    /// Instantiates a charged projectile at the fire point.
    /// </summary>
    private void FireChargedProjectile()
    {
        GameObject chargedShot = Instantiate(chargedProjectilePrefab, firePoint.position, Quaternion.identity);
    }

    public void OnFocusChanged(Focus newFocus)
    {
        switch (newFocus) {
            case Focus.Low:
                reloadTime = 3f;
                normalShotAvailable = false;
                chargedShotAvailable = false;
                break;
            case Focus.Normal:
                reloadTime = 2f;
                normalShotAvailable = true;
                chargedShotAvailable = false;
                break;
            case Focus.Medium:
                reloadTime = 1f;
                normalShotAvailable = true;
                chargedShotAvailable = false;
                break;
            case Focus.High:
                reloadTime = 0.5f;
                normalShotAvailable = true;
                chargedShotAvailable = true;
                chargeReloadTimer = 7.0f;
                break;
        }
        reloadSlider.maxValue = reloadTime;
    }
}
