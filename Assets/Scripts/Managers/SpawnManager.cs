using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class SpawnManager : MonoBehaviour
{
    public static event Action onScissorsSpawn;
    public GameObject[] origamiPrefabs;
    public GameObject scissorsPrefab;
    public SpawnSettings spawnSettings;
    private Coroutine spawnLoop;
    private float SPAWN_HIGHT_OFFSET = 1.3f;
    private bool spawnLoopRunning = false;

    void Start()
    {
        StartSpawnLoop();
        GameManager.onGameOver += StopSpawnLoop;
    }

    void Spawn()
    {
        bool isScissors = spawnSettings.scissorsSpawnPercent >= Random.Range(0f, 100f);

        Vector3 position = VectorUtils.GenerateRandomSpawnPointAboveScreen(SPAWN_HIGHT_OFFSET);
        GameObject unit = isScissors ? SpawnScissors(position) : SpawnOrigami(position);
    }

    GameObject SpawnScissors(Vector3 position)
    {
        onScissorsSpawn?.Invoke();
        return Instantiate(scissorsPrefab, position, scissorsPrefab.transform.rotation);
    }

    GameObject SpawnOrigami(Vector3 position)
    {
        GameObject origamiPrefab = origamiPrefabs[Random.Range(0, origamiPrefabs.Length)];
        return Instantiate(origamiPrefab, position, origamiPrefab.transform.rotation);
    }

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
        float spawnRate = spawnSettings.startSpawnRate;
        while (spawnLoopRunning)
        {
            Spawn();
            float spawnInterval = 1 / spawnRate;
            yield return new WaitForSeconds(spawnInterval);
            // adjust spawnRate based on spawnIncreaseRate
            float exponentialSpawnLimiter = (float)Mathf.Pow(2, spawnRate);
            float spawnRateModerator = spawnSettings.spawnIncreaseRate / (100f * exponentialSpawnLimiter) + 1f;
            spawnRate *= spawnRateModerator;
        }
    }

    void OnDisable()
    {
        StopSpawnLoop();
        GameManager.onGameOver -= StopSpawnLoop;
    }
}
