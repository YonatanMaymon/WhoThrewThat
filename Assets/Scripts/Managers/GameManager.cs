using System;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance { get; private set; }
    public static event Action onGameOver;
    public static float ScreenBufferX = 0.05f;
    public Dictionary<Enums.STATS, float> statsEffectivenessModerator { get; private set; } = new();

    public float gravityModerator = 1f;
    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
        DontDestroyOnLoad(gameObject);
    }
    private void OnEnable()
    {
        DataManager.onStatsUpdate += UpdateStatsEffectiveness;
        PlayerController.onScissorsCatch += OnGameOver;
    }

    void Start()
    {
        Physics.gravity *= gravityModerator;
    }

    public void UpdateStatsEffectiveness()
    {
        DataManager dataManager = DataManager.instance;
        ItemLoader itemLoader = ItemLoader.instance;
        if (dataManager == null || itemLoader == null)
            throw new WarningException("no DataManager or ItemLoader, stats are inactive");

        foreach (var stat in dataManager.statsLevels)
        {
            float statEffectivenessPerUpgrade = itemLoader.statsEffectiveness[stat.Key];
            float statModerator = 1 + statEffectivenessPerUpgrade / 100;
            statsEffectivenessModerator[stat.Key] = Mathf.Pow(statModerator, stat.Value);
        }
    }

    public void ExitGame()
    {
        DataManager.instance?.SaveData();
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }


    private void OnGameOver()
    {
        onGameOver?.Invoke();
    }

    private void OnDisable()
    {
        PlayerController.onScissorsCatch -= OnGameOver;
        DataManager.onStatsUpdate -= UpdateStatsEffectiveness;
    }

}
