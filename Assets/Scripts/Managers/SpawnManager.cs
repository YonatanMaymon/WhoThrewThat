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
    private bool spawnLoopRunning = false;

    void Start()
    {
        StartSpawnLoop();
        MainManager.onGameOver += StopSpawnLoop;
    }

    void Spawn()
    {
        bool isScissors = spawnSettings.scissorsChance >= Random.Range(0f, 1f);

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
        StartCoroutine(DeficultyIncreaseCoroutine());
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
            float spawnInterval = 1 / spawnSettings.spawnRate;
            Debug.Log("Spawn Interval: " + spawnInterval);
            yield return new WaitForSeconds(spawnInterval);
        }
    }
    IEnumerator DeficultyIncreaseCoroutine()
    {
        while (spawnLoopRunning)
        {
            // Increase spawn rate and scissors chance over time
            spawnSettings.spawnRate += spawnSettings.spawnRateIncrease / spawnSettings.spawnRate;
            spawnSettings.scissorsChance += (1 / spawnSettings.scissorsChance - 1) * spawnSettings.scissorsChanceIncrease;
            Debug.Log("scissorsChance: " + spawnSettings.scissorsChance);
            yield return new WaitForSeconds(1);
        }
    }

    void OnDisable()
    {
        StopSpawnLoop();
        MainManager.onGameOver -= StopSpawnLoop;
    }
}
