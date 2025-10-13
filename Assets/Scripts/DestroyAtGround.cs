using UnityEngine;

public class DestroyAtGround : MonoBehaviour
{
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Ground"))
            Destroy(gameObject);
    }
}
