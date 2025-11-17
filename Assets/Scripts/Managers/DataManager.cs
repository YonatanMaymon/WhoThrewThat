using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class DataManager : MonoBehaviour
{
    [Serializable]
    class StatLevel
    {
        public Enums.STATS stat;
        public int level = 0;
    }

    [Serializable]
    class GameData
    {
        public int coins = 0;
        public List<StatLevel> statsLevels = new();
    }
    private const string JsonFileName = "/save_data.json";

    public static DataManager instance { get; private set; }
    public Dictionary<Enums.STATS, int> statsLevels = new();

    public int coinAmount { get; private set; } = 0;
    public int coinsGained { get; private set; } = 0;
    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
        DontDestroyOnLoad(gameObject);
        LoadDefaultData();
        LoadData();
    }

    public void UpgradeStat(ShopItem shopItem)
    {
        if (coinAmount < shopItem.upgradePrice)
            return;
        statsLevels[shopItem.statType]++;
        coinAmount -= shopItem.upgradePrice;
    }

    public void IncrementCoins(int amount)
    {
        coinsGained = amount;
        coinAmount += amount;
    }

    public void SaveData()
    {
        GameData data = new GameData { coins = coinAmount };
        foreach (var stat in statsLevels)
        {
            data.statsLevels.Add(new StatLevel { stat = stat.Key, level = stat.Value });
        }
        string savePath = Application.persistentDataPath + JsonFileName;
        string json = JsonUtility.ToJson(data);
        File.WriteAllText(savePath, json);
    }

    private void LoadDefaultData()
    {
        foreach (Enums.STATS stat in Enum.GetValues(typeof(Enums.STATS)))
        {
            statsLevels.Add(stat, 0);
        }
    }

    public void LoadData()
    {
        string savePath = Application.persistentDataPath + JsonFileName;
        if (!File.Exists(savePath)) return;

        string json = File.ReadAllText(savePath);
        GameData data = JsonUtility.FromJson<GameData>(json);
        coinAmount = data.coins;
        foreach (var item in data.statsLevels)
        {
            statsLevels[item.stat] = item.level;
        }
    }
}
