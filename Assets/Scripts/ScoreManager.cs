using TMPro;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public TextMeshProUGUI scoreTextMesh;
    private int _score = 0;

    private void Start()
    {
        UpdateScore();
    }

    private void OnEnable()
    {
        PlayerController.onOrigamiCatch += AddScore;
    }

    private void OnDisable()
    {
        PlayerController.onOrigamiCatch -= AddScore;
    }

    private void AddScore(int score)
    {
        _score += score;
        UpdateScore();
    }

    private void UpdateScore()
    {
        scoreTextMesh.SetText("Score: " + _score);
    }
}
