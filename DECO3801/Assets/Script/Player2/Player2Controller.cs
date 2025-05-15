using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;

/// <summary>
/// Controls the player�s movement between fixed vertical lanes and shooting projectiles.
/// </summary>
public class Player2Controller : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private Transform firePoint;
    [SerializeField] private float reloadTime = 0.5f;
    private float timer = 0f;
    private bool canFire = true;

    /// <summary>
    /// Updates the player each frame by handling shooting.
    /// </summary>
    private void Update()
    {
        HandleShooting();
    }

    /// <summary>
    /// Handles input and reload logic for shooting projectiles.
    /// </summary>
    private void HandleShooting()
    {
        timer += Time.deltaTime;
        if (timer > reloadTime)
        {
            canFire = true;
            timer = 0f;
        }

        if (Input.GetKeyDown(KeyCode.P) && canFire)
        {
            canFire = false;
            FireProjectile();
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
