using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject gameOverScreen;
    public float gravityModerator = 1f;
    public static float ScreenBufferX = 0.05f;
    public static GameManager instance { get; private set; }

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
    }

    void Start()
    {
        Physics.gravity *= gravityModerator;
        PlayerController.onScissorsCatch += OnGameOver;
    }

    private void OnGameOver()
    {
        gameOverScreen.SetActive(true);
    }

    private void OnDestroy()
    {
        if (instance == this)
            instance = null;
    }
}
