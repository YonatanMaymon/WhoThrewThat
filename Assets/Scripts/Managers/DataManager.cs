using System.IO;
using UnityEngine;

public class DataManager : MonoBehaviour
{
    class GameData
    {
        public float coinAmount = 0;
    }
    public static DataManager instance { get; private set; }
    public float coinAmount { get; private set; } = 0;
    private const string JsonFileName = "/save_data.json";
    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
        DontDestroyOnLoad(gameObject);
        LoadData();
    }
    public void AddToCoins(int amount)
    {
        coinAmount += amount;
        Debug.Log(coinAmount);
    }

    public void SaveData()
    {
        string savePath = Application.persistentDataPath + JsonFileName;
        GameData data = new();
        data.coinAmount = coinAmount;
        string json = JsonUtility.ToJson(data);
        File.WriteAllText(savePath, json);
    }

    public void LoadData()
    {
        string savePath = Application.persistentDataPath + JsonFileName;
        if (!File.Exists(savePath)) return;

        string json = File.ReadAllText(savePath);
        GameData data = JsonUtility.FromJson<GameData>(json);
        coinAmount = data.coinAmount;
    }
}
