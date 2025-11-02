using System;
using UnityEngine;
using UnityEngine.UIElements;

public class GameUI : MonoBehaviour
{
    VisualElement root;
    Label scoreLabel;
    private void OnEnable()
    {
        root = GetComponent<UIDocument>().rootVisualElement;
        scoreLabel = root.Q<Label>("Score");
        if (scoreLabel == null)
            throw new InvalidOperationException();
        ScoreManager.onScoreUpdate += OnScoreUpdate;
    }

    private void OnScoreUpdate(int amount)
    {
        scoreLabel.text = "Score: " + amount;
    }

    private void OnDisable()
    {
        ScoreManager.onScoreUpdate -= OnScoreUpdate;
    }
}
