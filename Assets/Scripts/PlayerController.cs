using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
/*this script is supposed to do the following:
* - "collect" animals and snakes
* - the player should be thrown by the swipe of the pointer (click and drag, faster = more)
* - clamp the player position in screen
*/
public class PlayerController : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public float force = 10f;
    public float holdTime = 0.2f;
    private Rigidbody Rb;
    private float zDistance;
    private bool isPointerDown = false;

    void Start()
    {

        Rb = GetComponent<Rigidbody>();

        // the distance of the player from the camera in the z axis
        // (needed to calculate the mouse location)
        zDistance = Mathf.Abs(Camera.main.transform.position.z - transform.position.z);
    }

    private void Update()
    {
        MoveOnDrag();
        KeepOnScreen();
    }

    private void KeepOnScreen()
    {
        Vector3 LeftDown = Camera.main.ScreenToWorldPoint(new Vector3(0, 0, zDistance));
        Vector3 RightUp = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, zDistance));
        float x = Mathf.Clamp(transform.position.x, LeftDown.x, RightUp.x);
        float y = Mathf.Clamp(transform.position.y, LeftDown.y, RightUp.y);
        if (x != transform.position.x)
            Rb.linearVelocity = new Vector3(0, Rb.linearVelocity.y, 0);
        if (y != transform.position.y)
            Rb.linearVelocity = new Vector3(Rb.linearVelocity.x, 0, 0);
        transform.position = new Vector3(x, y, transform.position.z);
    }
    private void MoveOnDrag()
    {
        if (isPointerDown)
        {
            Vector2 mousePos = Mouse.current.position.ReadValue();
            // Convert to world space
            Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(
                new Vector3(mousePos.x, mousePos.y, zDistance)
            );
            Vector3 direction = (mouseWorldPos - transform.position).normalized;
            float sqrMagnitude = (mouseWorldPos - transform.position).sqrMagnitude;
            // add force in that direction
            Rb.linearVelocity = direction * sqrMagnitude * force;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Animal")) { }
        else if (other.gameObject.CompareTag("Snake")) { }
        else return;
        Destroy(other.gameObject);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        isPointerDown = true;
        StartCoroutine(releaseTimeoutCoroutine());
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        isPointerDown = false;
    }

    IEnumerator releaseTimeoutCoroutine()
    {
        yield return new WaitForSeconds(holdTime);
        isPointerDown = false;
    }
}
