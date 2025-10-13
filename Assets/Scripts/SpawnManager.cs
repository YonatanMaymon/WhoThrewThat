using System.Collections;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject[] animalsPrefabs;
    public GameObject snakePrefab;
    public float snakeSpawnPercent = 20;

    [Tooltip("starting spawn rate per second")]
    public float startSpawnRate = 0.5f;
    [Tooltip("how much the spawn rate increases each spawn by percentage")]
    public float spawnIncreaseRate = 2f;
    [Tooltip("the length on each side of the x axis an object will spawn")]
    public float spawnReach = 23f;
    public float spawnHight = 20f;
    private bool spawnLoopRunning = false;
    private Coroutine spawnLoop;

    void Start()
    {

        StartSpawnLoop();
    }

    void Update()
    {

    }
    /// <summary>
    /// spawn either a snake or a animal based on snakeSpawnPercent
    /// </summary>
    void Spawn()
    {
        // isSnake chance to be  true is exactly snakeSpawnPercent
        bool isSnake = snakeSpawnPercent >= Random.Range(0f, 100f);
        if (isSnake)
            SpawnSnake();
        else
            SpawnAnimal();
    }
    void SpawnSnake()
    {
        Instantiate(snakePrefab, GenerateRandomSpawnPoint(), snakePrefab.transform.rotation);
    }
    void SpawnAnimal()
    {
        GameObject animalPrefab = animalsPrefabs[Random.Range(0, animalsPrefabs.Length)];
        Instantiate(animalPrefab, GenerateRandomSpawnPoint(), animalPrefab.transform.rotation);
    }

    Vector3 GenerateRandomSpawnPoint()
    {
        float x = Random.Range(-spawnReach, spawnReach);
        return new Vector3(x, spawnHight, 0);
    }

    //------------------------------------SpawnLoop------------------------------------
    void StartSpawnLoop()
    {
        spawnLoopRunning = true;
        spawnLoop = StartCoroutine(SpawnLoopCoroutine());
    }
    void StopSpawnLoop()
    {
        spawnLoopRunning = false;
        if (spawnLoop != null)
            StopCoroutine(spawnLoop);
    }
    IEnumerator SpawnLoopCoroutine()
    {
        float spawnRate = startSpawnRate;
        while (spawnLoopRunning)
        {
            Spawn();
            float spawnInterval = 1 / spawnRate;
            yield return new WaitForSeconds(spawnInterval);
            // adjust spawnRate based on spawnIncreaseRate
            float exponentialSpawnLimiter = (float)Mathf.Pow(2, spawnRate);
            float spawnRateModerator = spawnIncreaseRate / (100f * exponentialSpawnLimiter) + 1f;
            spawnRate *= spawnRateModerator;
        }
    }
    void OnDisable()
    {
        StopSpawnLoop();
    }
}
