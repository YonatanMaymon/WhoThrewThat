using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static event Action onGameOver;
    public float gravityModerator = 1f;
    public static float ScreenBufferX = 0.05f;

    void Start()
    {
        Physics.gravity *= gravityModerator;
        PlayerController.onScissorsCatch += OnGameOver;
    }

    private void OnGameOver()
    {
        onGameOver?.Invoke();
    }

    private void OnDisable()
    {
        PlayerController.onScissorsCatch -= OnGameOver;
    }

}
