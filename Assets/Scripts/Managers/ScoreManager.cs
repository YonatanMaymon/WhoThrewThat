using System;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public static event Action<int> onScoreUpdate;
    private const int ScoreForCoin = 25;
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
        DataManager.instance.AddToCoins(_score / ScoreForCoin);
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
