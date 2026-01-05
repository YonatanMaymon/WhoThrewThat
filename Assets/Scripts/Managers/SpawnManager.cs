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

    private bool gamePlaying = true;
    private void OnEnable()
    {
        MainManager.onGameOver += () => { gamePlaying = false; };
    }
    void Start()
    {
        StartCoroutine(SpawnLoopCoroutine());
    }

    void Spawn()
    {
        float scissorsSpawnChance =
            ProgressionManager.ProgressionAdjustor(spawnSettings.scissorsChance, spawnSettings.maxScissorsChance);
        bool isScissors = scissorsSpawnChance >= Random.Range(0f, 1f);
        Debug.Log(scissorsSpawnChance);
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

    IEnumerator SpawnLoopCoroutine()
    {
        while (gamePlaying)
        {
            Spawn();
            float spawnRate =
                ProgressionManager.ProgressionAdjustor(spawnSettings.spawnRate, spawnSettings.maxSpawnRate);
            float spawnInterval = 1 / spawnRate;
            Debug.Log("Spawn Interval: " + spawnInterval);
            yield return new WaitForSeconds(spawnInterval);
        }
    }
}
