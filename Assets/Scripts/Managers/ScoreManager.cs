using System;
using TMPro;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public static event Action<int> onScoreUpdate;
    private int _score = 0;

    private void Start()
    {
        UpdateScore();
    }

    private void OnEnable()
    {
        PlayerController.onOrigamiCatch += AddScore;
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
    }
}
