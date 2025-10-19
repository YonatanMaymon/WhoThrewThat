using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static event Action onStopGame;
    public GameObject gameOverScreen;
    public float gravityModerator = 1f;
    public static float ScreenBufferX = 0.05f;

    void Start()
    {
        Physics.gravity *= gravityModerator;
        PlayerController.onScissorsCatch += OnGameOver;
    }
    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    private void OnGameOver()
    {
        gameOverScreen.SetActive(true);
        onStopGame?.Invoke();
    }


}
