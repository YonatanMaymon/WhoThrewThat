using UnityEngine;

public class DestroyOutBounds : MonoBehaviour
{
    private float boundY = -5;
    void Update()
    {
        if (transform.position.y < boundY)
            Destroy(gameObject);
    }
}
