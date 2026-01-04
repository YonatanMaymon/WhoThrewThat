using UnityEngine;

[System.Serializable]
public class SpawnSettings
{
    public float scissorsChance = 0.2f;
    public float scissorsChanceIncrease = 0.002f;

    [Tooltip("starting spawn rate per second")]
    public float spawnRate = 0.5f;
    [Tooltip("how much the spawn rate increases each second")]
    public float spawnRateIncrease = 0.02f;
}