using System;
using System.Collections;
using UnityEngine;

public class ProgressionManager : MonoBehaviour
{
    public static event Action onProgressUpdate;
    private static ProgressionManager instance;
    // Progress from 0 to 1 (0% to 100%) used for controlling difficulty increase, music, etc.
    private static float progress = 0f;
    private const float PROGRESSION_INCREASE_RATE = 0.002f; // Progress increase per second
    private bool gamePlaying = true;

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
    }
    private void OnEnable()
    {
        MainManager.onGameOver += OnGameOver;
    }

    private void Start()
    {
        StartCoroutine(ProgressCoroutine());
    }

    private void OnGameOver()
    {
        gamePlaying = false;
    }

    IEnumerator ProgressCoroutine()
    {
        while (gamePlaying)
        {
            yield return new WaitForSeconds(1);
            progress += PROGRESSION_INCREASE_RATE;
            progress = Mathf.Clamp01(progress);
            onProgressUpdate?.Invoke();
        }
        progress = 0f;
        onProgressUpdate?.Invoke();
    }

    /// <summary>
    /// Returns a value adjusted by the progression factor
    /// </summary>
    public static float ProgressionAdjustor(float stat, float maxStat)
    {
        return stat + (maxStat - stat) * progress;
    }

}