using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private GameObject[] spawnPrefabs;
    [SerializeField] private Transform[] spawnPoints;
    [SerializeField] private GameObject warningPrefab;
    [SerializeField] private float spawnInterval = 2f;

    private float timer = 0f;
    private void Update()
    {
        timer += Time.deltaTime;

        if (timer >= spawnInterval)
        {
            Spawn();
            timer = 0f;
        }
    }

    private void Spawn()
    {
        int randomPrefabIndex = Random.Range(0, spawnPrefabs.Length); // Pick a random prefab
        int randomSpawnIndex = Random.Range(0, spawnPoints.Length); // Pick a random spawn location
        if (randomPrefabIndex == 0)
        {
            SpawnAsteroid(randomSpawnIndex);
        }
        else
        {
            SpawnEnemy(randomPrefabIndex, randomSpawnIndex);
        }
    }

    private void SpawnAsteroid(int spawnIdx)
    {
        StartCoroutine(SpawnAsteroidWithWarning(spawnIdx));
    }

    private void SpawnEnemy(int randomPrefabIndex, int spawnIdx)
    {

        Instantiate(spawnPrefabs[spawnIdx], spawnPoints[spawnIdx].position, Quaternion.identity);
    }

    private IEnumerator SpawnAsteroidWithWarning(int spawnIdx)
    {
        //Spawn Warning
        Vector3 spawnPosition = spawnPoints[spawnIdx].position;
        GameObject warning = Instantiate(warningPrefab, spawnPosition, Quaternion.identity);
        warning.transform.SetParent(spawnPoints[spawnIdx]);

        // Flash the warning 3 times
        SpriteRenderer warningRenderer = warning.GetComponent<SpriteRenderer>();
        Color color = warningRenderer.color;
        for (int i = 0; i < 3; i++)
        {
            yield return new WaitForSeconds(0.1f); 
            color.a = 0f;
            warningRenderer.color = color;
            yield return new WaitForSeconds(0.1f); 
            color.a = 1f;
            warningRenderer.color = color;
        }
        Destroy(warning);
        yield return new WaitForSeconds(0.5f);

        //Spawn Asteroid
        spawnPosition = spawnPoints[spawnIdx].position;
        int ranY = spawnPoints[spawnIdx].position.y <= 0 ? Random.Range(0, 3) : Random.Range(-3, 0);
        Vector2 ranDir = new Vector2(Random.Range(-3, -1), ranY);
        Obstacle obstacle = Instantiate(spawnPrefabs[0], spawnPosition, Quaternion.identity).GetComponent<Obstacle>();
        obstacle.Initialize(ranDir);

    }

    private void GenerateRandomPos()
    {

    }
}
