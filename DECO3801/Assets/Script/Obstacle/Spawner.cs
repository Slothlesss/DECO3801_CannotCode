using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct spawnInterval
{
    public SpawnableObject obj;
    public int interval;
}

/// <summary>
/// Spawner is responsible for spawning various game objects at configured intervals and positions,
/// with some object-specific behavior (e.g., warnings for asteroids).
/// </summary>
public class Spawner : MonoBehaviour
{
    [SerializeField] private GameObject[] spawnPrefabs;

    private float timer = 0f;
    public spawnInterval[] spawnIntervals;
    [SerializeField] private Transform[] spawnPoints; //Later will use random instead of predefined pos
    [SerializeField] private GameObject warningPrefab;

    private Dictionary<SpawnableObject, GameObject> prefabDictionary;

    private Dictionary<SpawnableObject, float> intervalDictionary;
    private Dictionary<SpawnableObject, float> nextSpawnTimes;

    [SerializeField] private Difficulty currentDifficulty;

    /// <summary>
    /// Initializes dictionaries for spawning logic.
    /// </summary>
    private void Start()
    {
        // Initialize the spawnable object dictionary
        prefabDictionary = new Dictionary<SpawnableObject, GameObject>
        {
            { SpawnableObject.Asteroid, spawnPrefabs[0] },
            { SpawnableObject.Planet, spawnPrefabs[1] },
            { SpawnableObject.AsteroidBelt, spawnPrefabs[2] },
            { SpawnableObject.Coin, spawnPrefabs[3] }
        };


        // Initialize the interval dictionary
        intervalDictionary = new Dictionary<SpawnableObject, float>();
        foreach (var spawnInterval in spawnIntervals)
        {
            intervalDictionary[spawnInterval.obj] = spawnInterval.interval;
        }

        // Initialize the nextSpawnTimes dictionary
        nextSpawnTimes = new Dictionary<SpawnableObject, float>();
        foreach (var spawnInterval in spawnIntervals)
        {
            nextSpawnTimes[spawnInterval.obj] = spawnInterval.interval;
        }
    }

    /// <summary>
    /// Handles spawn timing and calls spawn methods as needed.
    /// </summary>
    private void Update()
    {
        timer += Time.deltaTime;

        int randomPrefabIndex = Random.Range(0, spawnPrefabs.Length); // Pick a random prefab
        SpawnableObject spawnType = (SpawnableObject)randomPrefabIndex;

        if (timer >= nextSpawnTimes[spawnType])
        {
            Spawn(spawnType);
            nextSpawnTimes[spawnType] = timer + intervalDictionary[spawnType];
        }
    }

    /// <summary>
    /// Dispatches the spawn logic based on object type.
    /// </summary>
    /// <param name="spawnType">The type of object to spawn.</param>
    private void Spawn(SpawnableObject spawnType)
    {
        int randomSpawnIndex = Random.Range(0, spawnPoints.Length); // Pick a random spawn location
        switch (spawnType)
        {
            case SpawnableObject.Asteroid:
                SpawnAsteroid(randomSpawnIndex);
                break;
            case SpawnableObject.Planet:
                SpawnPlanet();
                break;
            case SpawnableObject.AsteroidBelt:
                SpawnAsteroidBelt();
                break;
            case SpawnableObject.Coin:
                SpawnCoin();
                break;
        }
    }

    /// <summary>
    /// Triggers coroutine to spawn an asteroid with a warning.
    /// </summary>
    private void SpawnAsteroid(int posIdx)
    {
        StartCoroutine(SpawnAsteroidWithWarning(posIdx));
    }

    /// <summary>
    /// Spawns a planet at a predefined lateral offset.
    /// </summary>
    private void SpawnPlanet()
    {
        int randomPosIndex = (Random.Range(0, 2) == 0) ? 0 : 2;
        Vector2 spawnPos = spawnPoints[randomPosIndex].position + new Vector3(30, 0, 0);
        Obstacle obstacle = Instantiate(prefabDictionary[SpawnableObject.Planet], spawnPos, Quaternion.identity).GetComponent<Obstacle>();
        obstacle.Initialize(new Vector2(0, 0));
    }

    /// <summary>
    /// Spawns a static asteroid belt at a random position.
    /// </summary>
    private void SpawnAsteroidBelt()
    {
        int randomPosIndex = Random.Range(0, 2);
        Vector2 spawnPos = spawnPoints[randomPosIndex].position + new Vector3(30, 0, 0);
        Obstacle obstacle = Instantiate(prefabDictionary[SpawnableObject.AsteroidBelt], spawnPos, Quaternion.identity).GetComponent<Obstacle>();
        obstacle.Initialize(new Vector2(0, 0)); //Not moving
    }

    /// <summary>
    /// Spawns a coin at a random spawn point.
    /// </summary>
    private void SpawnCoin()
    {
        int randomPosIndex = Random.Range(0, 2);
        Vector2 spawnPos = spawnPoints[randomPosIndex].position + new Vector3(10, 0, 0);
        Obstacle obstacle = Instantiate(prefabDictionary[SpawnableObject.Coin], spawnPos, Quaternion.identity).GetComponentInChildren<Obstacle>();
        obstacle.Initialize(new Vector2(0, 0));
    }

    /// <summary>
    /// Spawns an asteroid after a warning icon flashes. Asteroids are given a randomized movement vector.
    /// </summary>
    /// <param name="spawnIdx">Index of the spawn point to use.</param>
    private IEnumerator SpawnAsteroidWithWarning(int spawnIdx) //Later asteroid won't have warnings, rockets will have
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
        Obstacle obstacle = Instantiate(prefabDictionary[SpawnableObject.Asteroid], spawnPosition, Quaternion.identity).GetComponent<Obstacle>();
        obstacle.Initialize(ranDir);
    }

    public void SetDifficulty(Difficulty difficulty)
    {
        switch (difficulty)
        {
            case Difficulty.Easy:
                foreach (var spawnInterval in spawnIntervals)
                {
                    intervalDictionary[spawnInterval.obj] = spawnInterval.interval / 2;
                }
                break;

            case Difficulty.Medium:
                foreach (var spawnInterval in spawnIntervals)
                {
                    intervalDictionary[spawnInterval.obj] = spawnInterval.interval / 5;
                }
                break;

            case Difficulty.Hard:
                foreach (var spawnInterval in spawnIntervals)
                {
                    intervalDictionary[spawnInterval.obj] = spawnInterval.interval / 10;
                }
                break;
        }
    }
}
