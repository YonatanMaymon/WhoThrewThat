using UnityEngine;

public class GameManager : MonoBehaviour
{
    public float gravityModerator = 2.5f;
    public static float ScreenBufferX = 0.05f;

    void Start()
    {
        Physics.gravity *= gravityModerator;

    }

}
