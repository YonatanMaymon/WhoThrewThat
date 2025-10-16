using UnityEngine;

public class GameManager : MonoBehaviour
{
    public float gravityModerator = 2.5f;
    void Start()
    {
        Physics.gravity *= gravityModerator;

    }

}
