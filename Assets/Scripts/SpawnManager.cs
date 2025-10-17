using System.Collections;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    // USE FOR SOUNDS
    public delegate void UnitSpawnedHandler(GameObject gameObject);
    public static event UnitSpawnedHandler OnUnitSpawned;
    //--------------------------------------------------------------
    public GameObject[] origamiPrefabs;
    public GameObject scissorsPrefab;
    public float scissorsSpawnPercent = 20;

    [Tooltip("starting spawn rate per second")]
    public float startSpawnRate = 0.5f;
    [Tooltip("how much the spawn rate increases each spawn by percentage")]
    public float spawnIncreaseRate = 2f;
    private float SPAWN_HIGHT_OFFSET = 1.3f;
    private bool spawnLoopRunning = false;
    private Coroutine spawnLoop;

    void Start()
    {
        StartSpawnLoop();
    }

    // spawn a unit
    void Spawn()
    {
        bool isScissors = scissorsSpawnPercent >= Random.Range(0f, 100f);

        // spawn a unit
        Vector3 position = Util.GenerateRandomSpawnPointAboveScreen(SPAWN_HIGHT_OFFSET);
        GameObject unit = isScissors ? SpawnScissors(position) : SpawnOrigami(position);
        OnUnitSpawned?.Invoke(unit);
    }
    GameObject SpawnScissors(Vector3 position)
    {
        return Instantiate(scissorsPrefab, position, scissorsPrefab.transform.rotation);
    }
    GameObject SpawnOrigami(Vector3 position)
    {
        GameObject origamiPrefab = origamiPrefabs[Random.Range(0, origamiPrefabs.Length)];
        return Instantiate(origamiPrefab, position, origamiPrefab.transform.rotation);
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
