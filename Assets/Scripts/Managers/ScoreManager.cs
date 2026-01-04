using System;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public static event Action<int, int> onScoreUpdate;
    private const float baseCoinPerScore = 1f / 25f;
    private int _score = 0;
    private int _combo = 0;

    private void Start()
    {
        UpdateScore();
    }

    private void OnEnable()
    {
        PlayerController.onOrigamiCatch += AddScore;
        Origami.onFallOnGround += ResetCombo;
        MainManager.onGameOver += OnGameOver;
    }

    private void OnGameOver()
    {
        MainManager mainManager = MainManager.instance;

        float coinPerScore = mainManager != null ?
            baseCoinPerScore * mainManager.statsEffectivenessModerator[Enums.STATS.COIN_GAIN]
            :
            baseCoinPerScore;
        int coinsGained = (int)(_score * coinPerScore);
        DataManager.instance.IncrementCoins(coinsGained);
    }

    private void AddScore(int score)
    {
        float comboMultiplier = 1f + (_combo * 0.1f);
        _score += (int)(score * comboMultiplier);
        _combo++;
        UpdateScore();
    }

    private void ResetCombo()
    {
        _combo = 0;
        UpdateScore();
    }

    private void UpdateScore()
    {
        onScoreUpdate?.Invoke(_score, _combo);
    }
    private void OnDisable()
    {
        PlayerController.onOrigamiCatch -= AddScore;
        Origami.onFallOnGround -= ResetCombo;
        MainManager.onGameOver -= OnGameOver;
    }
}
