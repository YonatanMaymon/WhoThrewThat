using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;
using Consts = UIConsts.Game;

public class GameUI : MonoBehaviour
{
    private int _score = 0;
    VisualElement root;
    VisualElement gameOverUIContainer;
    Label updatingScoreLabel;
    Label finalScoreLabel;
    Button restartButton;
    Button menuButton;
    private void OnEnable()
    {
        AssignUIVariables();

        ScoreManager.onScoreUpdate += OnScoreUpdate;
        GameManager.onGameOver += OnGameOver;
        restartButton.clicked += OnRestartClick;
        menuButton.clicked += OnMenuClick;
    }

    private void OnGameOver()
    {
        finalScoreLabel.text = "" + _score;
        updatingScoreLabel.AddToClassList(Consts.HideClass);
        gameOverUIContainer.RemoveFromClassList(Consts.HideClass);
    }

    private void AssignUIVariables()
    {
        root = GetComponent<UIDocument>().rootVisualElement;
        gameOverUIContainer = root.Q<VisualElement>(Consts.GameOverUIContainerName);
        updatingScoreLabel = root.Q<Label>(Consts.UpdatingScoreName);
        finalScoreLabel = root.Q<Label>(Consts.FinalScoreName);
        restartButton = root.Q<Button>(Consts.RestartButtonName);
        menuButton = root.Q<Button>(Consts.MenuButtonName);

        if (gameOverUIContainer == null || updatingScoreLabel == null || finalScoreLabel == null || restartButton == null || menuButton == null)
            throw new InvalidOperationException("UI elements name is different the the ones defined in UIConsts");

    }

    private void OnScoreUpdate(int score)
    {
        _score = score;
        updatingScoreLabel.text = "Score: " + score;
    }

    private void OnMenuClick()
    {
        SceneManager.LoadScene((int)Enums.SCENES.MENU);
    }

    private void OnRestartClick()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }


    private void OnDisable()
    {
        ScoreManager.onScoreUpdate -= OnScoreUpdate;
        GameManager.onGameOver -= OnGameOver;
        restartButton.clicked -= OnRestartClick;
        menuButton.clicked -= OnMenuClick;
    }
}
