using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;
using Consts = UIConsts.Game;

public class GameUI : MonoBehaviour
{
    private int _score = 0;
    VisualElement root, gameOverUIContainer;
    Label updatingScoreLabel, finalScoreLabel, coinAmountLabel;
    Button restartButton, menuButton;

    private void Awake()
    {
        AssignVariables();
    }

    private void OnEnable()
    {
        ScoreManager.onScoreUpdate += OnScoreUpdate;
        GameManager.onGameOver += OnGameOver;
        restartButton.clicked += OnRestartClick;
        menuButton.clicked += OnMenuClick;
    }

    private void OnGameOver()
    {
        finalScoreLabel.text = "" + _score;
        coinAmountLabel.text = "" + DataManager.instance.coinsGained;
        updatingScoreLabel.AddToClassList(Consts.HideClass);
        gameOverUIContainer.RemoveFromClassList(Consts.HideClass);
    }

    private void AssignVariables()
    {
        root = GetComponent<UIDocument>().rootVisualElement;
        gameOverUIContainer = root.Q<VisualElement>(Consts.GameOverUIContainerName);
        updatingScoreLabel = root.Q<Label>(Consts.UpdatingScoreName);
        finalScoreLabel = root.Q<Label>(Consts.FinalScoreName);
        coinAmountLabel = root.Q<Label>(Consts.CoinAmountName);
        restartButton = root.Q<Button>(Consts.RestartButtonName);
        menuButton = root.Q<Button>(Consts.MenuButtonName);

        if (gameOverUIContainer == null || updatingScoreLabel == null || finalScoreLabel == null || coinAmountLabel == null || restartButton == null || menuButton == null)
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
