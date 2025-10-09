using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public float speed = 5f;
    private InputAction clickAction;
    private float zDistance;

    void Start() {
        clickAction = InputSystem.actions.FindAction("Click");
        // the distance of the player from the camera in the z axis
        // (needed to calculate the mouse location)
        zDistance = Mathf.Abs(Camera.main.transform.position.z - transform.position.z);
    }

    void Update()
    {
        Vector2 mouseScreenPos = Mouse.current.position.ReadValue();

        // Convert to world space
        Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(
            new Vector3(mouseScreenPos.x, mouseScreenPos.y, zDistance)
        );
        transform.position = Vector3.Lerp(transform.position, mouseWorldPos, speed * Time.deltaTime);
    }


}
