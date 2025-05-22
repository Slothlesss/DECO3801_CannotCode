using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Spawner is responsible for spawning various game objects at configured intervals and positions,
/// with some object-specific behavior (e.g., warnings for asteroids).
/// </summary>
public class Spawner : MonoBehaviour
{
    [SerializeField] private GameObject[] spawnPrefabs;

    private float timer = 0f;
    [SerializeField] private Transform[] asteroidSpawnPos;
    [SerializeField] private Transform[] planetSpawnPos;
    [SerializeField] private Transform[] asteroidClusterSpawnPos;
    [SerializeField] private Transform[] asteroidBeltPos;
    [SerializeField] private Transform[] coinPos;
    [SerializeField] private GameObject warningPrefab;

    private Dictionary<SpawnableObject, GameObject> prefabDictionary;
    private Dictionary<SpawnableObject, float> nextSpawnTimes;

    private Dictionary<Frustration, Dictionary<SpawnableObject, bool>> frustrationConfigs;
    private Dictionary<Fatigue, Dictionary<SpawnableObject, float>> fatigueConfigs;
    private Dictionary<Focus, Dictionary<SpawnableObject, float>> focusConfigs;
    /// <summary>
    /// Initializes dictionaries for spawning logic.
    /// </summary>
    private void Start()
    {
        InitializeFrustrationConfigs();
        InitializeFatigueConfigs();
        InitializeFocusConfigs();

        InitializePrefabDictionary();
        InitializeNextSpawnTimes();
    }
    private void InitializePrefabDictionary()
    {
        // Initialize the spawnable object dictionary
        // => make the code more understandable. For example prefabDictionary[SpawnableObject.Asteroid] instead of spawnPrefab[0].
        prefabDictionary = new Dictionary<SpawnableObject, GameObject>
        {
            { SpawnableObject.Asteroid, spawnPrefabs[0] },
            { SpawnableObject.Planet, spawnPrefabs[1] },
            { SpawnableObject.AsteroidCluster, spawnPrefabs[2] },
            { SpawnableObject.AsteroidBelt, spawnPrefabs[3] },
            { SpawnableObject.Coin, spawnPrefabs[4] }
        };
    }

    private void InitializeNextSpawnTimes()
    {
        nextSpawnTimes = new Dictionary<SpawnableObject, float>();
        foreach (var kvp in fatigueConfigs[GameManager.Instance.fatigue])
        {
            nextSpawnTimes[kvp.Key] = kvp.Value;
        }

        foreach (var kvp in focusConfigs[GameManager.Instance.focus])
        {
            nextSpawnTimes[kvp.Key] = kvp.Value;
        }

    }

    private void InitializeFrustrationConfigs()
    {
        frustrationConfigs = new Dictionary<Frustration, Dictionary<SpawnableObject, bool>>
        {
            [Frustration.Normal] = new Dictionary<SpawnableObject, bool>
        {
            { SpawnableObject.Asteroid, true},
            { SpawnableObject.Planet, false},
            { SpawnableObject.AsteroidCluster, false},
            { SpawnableObject.AsteroidBelt, false}
        },
            [Frustration.Mild] = new Dictionary<SpawnableObject, bool> 
        {
            { SpawnableObject.Asteroid, true},
            { SpawnableObject.Planet, true},
            { SpawnableObject.AsteroidCluster, false},
            { SpawnableObject.AsteroidBelt, false}
        },
            [Frustration.Moderate] = new Dictionary<SpawnableObject, bool>
        {
            { SpawnableObject.Asteroid, true},
            { SpawnableObject.Planet, true},
            { SpawnableObject.AsteroidCluster, true},
            { SpawnableObject.AsteroidBelt, false}
        },
            [Frustration.High] = new Dictionary<SpawnableObject, bool> {
            { SpawnableObject.Asteroid, true},
            { SpawnableObject.Planet, true},
            { SpawnableObject.AsteroidCluster, true},
            { SpawnableObject.AsteroidBelt, true},
        }
        };
    }

    private void InitializeFatigueConfigs()
    {
        fatigueConfigs = new Dictionary<Fatigue, Dictionary<SpawnableObject, float>>
        {
            [Fatigue.Normal] = new Dictionary<SpawnableObject, float>
        {
            { SpawnableObject.Asteroid, 3f },
            { SpawnableObject.Planet, 7f },
            { SpawnableObject.AsteroidCluster, 3f},
            { SpawnableObject.AsteroidBelt, 5f }
        },
            [Fatigue.Mild] = new Dictionary<SpawnableObject, float>
        {
            { SpawnableObject.Asteroid, 2f },
            { SpawnableObject.Planet, 7f },
            { SpawnableObject.AsteroidCluster, 3f},
            { SpawnableObject.AsteroidBelt, 5f }
        },
            [Fatigue.Moderate] = new Dictionary<SpawnableObject, float>
        {
            { SpawnableObject.Asteroid, 1f },
            { SpawnableObject.Planet, 7f },
            { SpawnableObject.AsteroidCluster, 3f},
            { SpawnableObject.AsteroidBelt, 5f }
        },
            [Fatigue.High] = new Dictionary<SpawnableObject, float>
        {
            { SpawnableObject.Asteroid, 0.5f },
            { SpawnableObject.Planet, 7f },
            { SpawnableObject.AsteroidCluster, 3f},
            { SpawnableObject.AsteroidBelt, 5f }
        }
        };
    }

    private void InitializeFocusConfigs()
    {
        focusConfigs = new Dictionary<Focus, Dictionary<SpawnableObject, float>>
        {
            [Focus.Low] = new Dictionary<SpawnableObject, float>
        {
            { SpawnableObject.Coin, 4f},
        },
            [Focus.Normal] = new Dictionary<SpawnableObject, float>
        {
            { SpawnableObject.Coin, 3f},
        },
            [Focus.Medium] = new Dictionary<SpawnableObject, float>
        {
            { SpawnableObject.Coin, 2f},
        },
            [Focus.High] = new Dictionary<SpawnableObject, float> 
        {
            { SpawnableObject.Coin, 1f},
        }
        };
    }



    /// <summary>
    /// Handles spawn timing and calls spawn methods as needed.
    /// </summary>
    private void Update()
    {
        timer += Time.deltaTime;

        int randomPrefabIndex = Random.Range(0, spawnPrefabs.Length - 1); // Pick a random prefab except coin
        SpawnableObject spawnType = (SpawnableObject)randomPrefabIndex;

        if (timer >= nextSpawnTimes[spawnType])
        {
            //Check frustration config
            if (frustrationConfigs[GameManager.Instance.frustration][spawnType])
            {
                Spawn(spawnType);
                //Check fatigue config
                nextSpawnTimes[spawnType] = timer + fatigueConfigs[GameManager.Instance.fatigue][spawnType];
            }
        }
        else if (timer >= nextSpawnTimes[SpawnableObject.Coin])
        {
            Spawn(SpawnableObject.Coin);
            //Check focus config
            nextSpawnTimes[SpawnableObject.Coin] = timer + focusConfigs[GameManager.Instance.focus][SpawnableObject.Coin];
        }
    }

    /// <summary>
    /// Dispatches the spawn logic based on object type.
    /// </summary>
    /// <param name="spawnType">The type of object to spawn.</param>
    private void Spawn(SpawnableObject spawnType)
    {
        switch (spawnType)
        {
            case SpawnableObject.Asteroid:
                SpawnAsteroid();
                break;
            case SpawnableObject.Planet:
                SpawnPlanet();
                break;
            case SpawnableObject.AsteroidCluster:
                SpawnAsteroidCluster();
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
    private void SpawnAsteroid()
    {
        int randomPosIndex = Random.Range(0, asteroidSpawnPos.Length); // Pick a random spawn location
        Vector2 spawnPosition = asteroidSpawnPos[randomPosIndex].position + new Vector3(0, 0, 0);

        int ranX = Random.Range(-2, -1);
        int ranY = asteroidSpawnPos[randomPosIndex].position.y <= 0 ? Random.Range(0, 3) : Random.Range(-3, 0);
        Vector2 ranDir = new Vector2(ranX, ranY) ;
        Obstacle obstacle = Instantiate(
            prefabDictionary[SpawnableObject.Asteroid],
            spawnPosition,
            Quaternion.identity
        ).GetComponent<Obstacle>();
        obstacle.Initialize(ranDir);
    }

    /// <summary>
    /// Spawns a planet at a predefined lateral offset.
    /// </summary>
    private void SpawnPlanet()
    {
        int randomPosIndex = Random.Range(0, planetSpawnPos.Length);
        Vector2 spawnPos = planetSpawnPos[randomPosIndex].position + new Vector3(30, 0, 0);
        Obstacle obstacle = Instantiate(prefabDictionary[SpawnableObject.Planet], spawnPos, Quaternion.identity).GetComponent<Obstacle>();
        obstacle.Initialize(new Vector2(0, 0));
    }

    /// <summary>
    /// Spawns a static asteroid cluster at a random position.
    /// </summary>
    private void SpawnAsteroidCluster()
    {
        int randomPosIndex = Random.Range(0, asteroidClusterSpawnPos.Length);
        Vector2 spawnPos = asteroidClusterSpawnPos[randomPosIndex].position + new Vector3(30, 0, 0);
        Obstacle obstacle = Instantiate(prefabDictionary[SpawnableObject.AsteroidCluster], spawnPos, Quaternion.identity).GetComponent<Obstacle>();
        obstacle.Initialize(new Vector2(0, 0));
    }


    /// <summary>
    /// Spawns a static asteroid belt at a random position.
    /// </summary>
    private void SpawnAsteroidBelt()
    {
        int randomPosIndex = Random.Range(0, asteroidBeltPos.Length);
        Vector2 spawnPos = asteroidBeltPos[randomPosIndex].position + new Vector3(30, 0, 0);
        Obstacle obstacle = Instantiate(prefabDictionary[SpawnableObject.AsteroidBelt], spawnPos, Quaternion.identity).GetComponent<Obstacle>();
        obstacle.Initialize(new Vector2(0, 0));
    }

    /// <summary>
    /// Spawns a coin at a random spawn point.
    /// </summary>
    private void SpawnCoin()
    {
        int randomPosIndex = Random.Range(0, coinPos.Length);
        Vector2 spawnPos = coinPos[randomPosIndex].position + new Vector3(10, 0, 0);
        Collectable collectable = Instantiate(prefabDictionary[SpawnableObject.Coin], spawnPos, Quaternion.identity).GetComponentInChildren<Collectable>();
        collectable.Initialize(new Vector2(0, 0));
    }

}
