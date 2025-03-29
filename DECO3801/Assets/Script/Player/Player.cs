using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour
{
    private int health = 3;
    [SerializeField] private GameObject heart1;
    [SerializeField] private GameObject heart2;
    [SerializeField] private GameObject heart3;
    private float targetTime = 5.0f;

    private void OnTriggerEnter2D(Collider2D other)
    {

        if (other.CompareTag("Enemy"))
        {
            if (health == 3)
            {
                heart3.SetActive(false);
            }
            else if (health == 2)
            {
                heart2.SetActive(false);
            }
            else if (health == 1)
            {
                heart1.SetActive(false);
            }
            startTimer();
            health -= 1;
        }
    }

    private void startTimer()
    {

    }
}
