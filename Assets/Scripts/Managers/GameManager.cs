using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance { get; private set; }
    public static event Action onGameOver;
    public static float ScreenBufferX = 0.05f;
    public float gravityModerator = 1f;
    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        Physics.gravity *= gravityModerator;
        PlayerController.onScissorsCatch += OnGameOver;
    }
    public void ExitGame()
    {
        DataManager.instance.SaveData();
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
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
