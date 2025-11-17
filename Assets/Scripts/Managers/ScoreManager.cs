using System;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public static event Action<int> onScoreUpdate;
    private const float baseCoinPerScore = 1f / 25f;
    private int _score = 0;

    private void Start()
    {
        UpdateScore();
    }

    private void OnEnable()
    {
        PlayerController.onOrigamiCatch += AddScore;
        GameManager.onGameOver += OnGameOver;
    }

    private void OnGameOver()
    {
        GameManager gameManager = GameManager.instance;

        float coinPerScore = gameManager != null ?
            baseCoinPerScore * gameManager.statsEffectivenessModerator[Enums.STATS.COIN_GAIN]
            :
            baseCoinPerScore;
        int coinsGained = (int)(_score * coinPerScore);
        DataManager.instance.IncrementCoins(coinsGained);
    }

    private void AddScore(int score)
    {
        _score += score;
        UpdateScore();
    }

    private void UpdateScore()
    {
        onScoreUpdate?.Invoke(_score);
    }
    private void OnDisable()
    {
        PlayerController.onOrigamiCatch -= AddScore;
        GameManager.onGameOver -= OnGameOver;
    }
}
