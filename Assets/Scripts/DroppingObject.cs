using System;
using UnityEngine;

public class DroppingObject : MonoBehaviour
{
    public float torqueStrength = 5f;
    private Rigidbody Rb;
    private void Start()
    {
        Rb = GetComponent<Rigidbody>();
        Rb.AddTorque(VectorUtils.GenerateRandomVector3(torqueStrength));
    }
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Ground"))
            FallOnGround();
    }

    protected virtual void FallOnGround()
    {
        Destroy(gameObject);
    }
}
