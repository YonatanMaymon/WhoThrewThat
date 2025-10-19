using UnityEngine;

[System.Serializable]
public class SpawnSettings
{
    public float scissorsSpawnPercent = 20;

    [Tooltip("starting spawn rate per second")]
    public float startSpawnRate = 0.5f;
    [Tooltip("how much the spawn rate increases each spawn by percentage")]
    public float spawnIncreaseRate = 2f;
}