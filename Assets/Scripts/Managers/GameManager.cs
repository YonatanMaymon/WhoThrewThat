using System;
using UnityEngine;

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

    private void OnGameOver()
    {
        gameOverScreen.SetActive(true);
        onStopGame?.Invoke();
    }


}
