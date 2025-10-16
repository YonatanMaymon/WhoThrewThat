using System;
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
    public static event Action onScissorsCatch;
    public static event Action<int> onOrigamiCatch;
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
        Vector3 LeftDown = Util.GetWorldSpacePos(0, 0);
        Vector3 RightUp = Util.GetWorldSpacePos(Screen.width, Screen.height);
        float x = Mathf.Clamp(transform.position.x, LeftDown.x, RightUp.x);
        if (x != transform.position.x)
            Rb.linearVelocity = new Vector3(0, Rb.linearVelocity.y, 0);
        if (transform.position.y >= RightUp.y)
            Rb.linearVelocity = new Vector3(Rb.linearVelocity.x, 0, 0);
        transform.position = new Vector3(x, transform.position.y, transform.position.z);
    }
    private void MoveOnDrag()
    {
        if (isPointerDown)
        {
            Vector2 mousePos = Mouse.current.position.ReadValue();
            // Convert to world space
            Vector3 mouseWorldPos = Util.GetWorldSpacePos(mousePos.x, mousePos.y);
            Vector3 directionVector = mouseWorldPos - transform.position;
            Vector3 direction = directionVector.normalized;
            float magnitude = directionVector.magnitude;
            // add force in that direction
            Rb.linearVelocity = direction * magnitude * force * Time.deltaTime;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Origami"))
        {
            onOrigamiCatch?.Invoke(other.GetComponent<Origami>().score);
        }
        else if (other.CompareTag("Scissors"))
        {
            onScissorsCatch?.Invoke();
        }
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
