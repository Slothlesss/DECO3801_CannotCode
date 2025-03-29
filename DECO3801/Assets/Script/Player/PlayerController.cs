using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float speed;
    private Rigidbody2D rb;

    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private Transform firePoint;
    [SerializeField] private float reloadTime = 0.5f;
    private float timer = 0f;
    private bool canFire = true;


    private float[] positionsY = { -3f, 1f, 5f }; // Allowed Y positions
    private int currentPositionIndex = 1; // Start at Y = 0
    private float moveDuration = 0.5f; // Duration of movement
    bool isMoving = false;


    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    private void Update()
    {

        HandleMovement();
        HandleShooting();
    }

    private void HandleMovement()
    {

        if (Input.GetKeyDown(KeyCode.W) && !isMoving)
        {
            StartCoroutine(MovePlayer(1));
        }
        else if (Input.GetKeyDown(KeyCode.S) && !isMoving)
        {
            StartCoroutine(MovePlayer(-1));
        }
    }

    private IEnumerator MovePlayer(int direction)
    {
        isMoving = true;
        int newPositionIndex = Mathf.Clamp(currentPositionIndex + direction, 0, positionsY.Length - 1);

        if (newPositionIndex == currentPositionIndex) // No change in position
            yield break;

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

        isMoving = false;
        // Ensure precise Y position at the end
        transform.position = new Vector3(transform.position.x, targetY, transform.position.z);
    }

    private void HandleShooting()
    {
        timer += Time.deltaTime;
        if (timer > reloadTime)
        {
            canFire = true;
            timer = 0f;
        }

        if (Input.GetKeyDown(KeyCode.Space) && canFire)
        {
            canFire = false;
            FireProjectile();
        }
    }

    private void FireProjectile()
    {
        GameObject projectile = Instantiate(projectilePrefab, firePoint.position, Quaternion.identity);
    }
    
    
}
