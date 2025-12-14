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
    private float SPAWN_HIGHT_OFFSET = 1.3f;
    private float spawnRate;
    private bool spawnLoopRunning = false;

    void Start()
    {
        spawnRate = spawnSettings.startSpawnRate;
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
        StartCoroutine(SpawnLoopCoroutine());
        StartCoroutine(SpawnIncreaseLoopCoroutine());
    }

    void StopSpawnLoop()
    {
        spawnLoopRunning = false;
    }

    IEnumerator SpawnLoopCoroutine()
    {
        while (spawnLoopRunning)
        {
            Spawn();
            float spawnInterval = 1 / spawnRate;
            yield return new WaitForSeconds(spawnInterval);
        }
    }
    IEnumerator SpawnIncreaseLoopCoroutine()
    {
        while (spawnLoopRunning)
        {
            spawnRate += spawnSettings.spawnRateIncrease / (2 * spawnRate);
            Debug.Log(spawnRate);
            yield return new WaitForSeconds(1);
        }
    }

    void OnDisable()
    {
        StopSpawnLoop();
        GameManager.onGameOver -= StopSpawnLoop;
    }
}
