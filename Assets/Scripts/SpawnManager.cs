using System.Collections;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject[] origamiPrefabs;
    public GameObject scissorsPrefab;
    public float scissorsSpawnPercent = 20;

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

    /// <summary>
    /// spawn either scissors or origami based on scissorsSpawnPercent
    /// </summary>
    void Spawn()
    {
        // isScissors chance to be  true is exactly scissorsSpawnPercent
        bool isScissors = scissorsSpawnPercent >= Random.Range(0f, 100f);
        if (isScissors)
            SpawnScissors();
        else
            SpawnOrigami();
    }
    void SpawnScissors()
    {
        Instantiate(scissorsPrefab, GenerateRandomSpawnPoint(), scissorsPrefab.transform.rotation);
    }
    void SpawnOrigami()
    {
        GameObject origamiPrefab = origamiPrefabs[Random.Range(0, origamiPrefabs.Length)];
        Instantiate(origamiPrefab, GenerateRandomSpawnPoint(), origamiPrefab.transform.rotation);
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
