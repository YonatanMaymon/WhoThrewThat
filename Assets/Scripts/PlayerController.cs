using System;
using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;


public class PlayerController : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public static event Action onScissorsCatch;
    public static event Action<int> onOrigamiCatch;
    [SerializeField]
    private float baseForce = 100f, baseGripTime = 0.2f;
    private float force, gripTime;
    private Rigidbody Rb;
    private bool isPointerDown = false;

    void Start()
    {
        Rb = GetComponent<Rigidbody>();
        force = baseForce * GameManager.instance.statsEffectivenessModerator[Enums.STATS.STRENGTH];
        gripTime = baseGripTime * GameManager.instance.statsEffectivenessModerator[Enums.STATS.GRIP];
    }

    private void Update()
    {
        MoveOnDrag();
        KeepOnScreen();
    }

    private void KeepOnScreen()
    {
        float offsetX = Screen.width * GameManager.ScreenBufferX;
        Vector3 leftDown = VectorUtils.GetWorldSpacePos(offsetX, 0);
        Vector3 rightDown = VectorUtils.GetWorldSpacePos(Screen.width - offsetX, 0);
        Vector3 up = VectorUtils.GetWorldSpacePos(Screen.width / 2, Screen.height);

        float x = Mathf.Clamp(transform.position.x, leftDown.x, rightDown.x);
        if (x != transform.position.x)
            Rb.linearVelocity = new Vector3(0, Rb.linearVelocity.y, 0);
        if (transform.position.y >= up.y)
            Rb.linearVelocity = new Vector3(Rb.linearVelocity.x, 0, 0);
        transform.position = new Vector3(x, transform.position.y, transform.position.z);
    }
    private void MoveOnDrag()
    {
        if (isPointerDown)
        {
            Vector2 mousePos = Mouse.current.position.ReadValue();
            // Convert to world space
            Vector3 mouseWorldPos = VectorUtils.GetWorldSpacePos(mousePos.x, mousePos.y);
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
        yield return new WaitForSeconds(gripTime);
        isPointerDown = false;
    }
}
