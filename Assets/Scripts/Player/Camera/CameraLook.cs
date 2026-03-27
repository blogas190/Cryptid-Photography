using UnityEngine;
using UnityEngine.InputSystem;

public class CameraLook : MonoBehaviour
{
    [Header("Input")]
    [SerializeField] private InputActionReference lookAction;

    [Header("Settings")]
    [SerializeField] private float sensitivity;
    [SerializeField] private float minPitch;
    [SerializeField] private float maxPitch;

    private float yaw;
    private float pitch;

    public static CameraLook instance;

    void Awake()
    {
        instance = this;
    }

    private void OnEnable()
    {
        lookAction.action.Enable();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void OnDisable()
    {
        lookAction.action.Disable();
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    private void Update()
    {
        Vector2 lookDelta = lookAction.action.ReadValue<Vector2>();

        yaw += lookDelta.x * sensitivity;
        pitch -= lookDelta.y * sensitivity;
        pitch = Mathf.Clamp(pitch, minPitch, maxPitch);

        transform.localRotation = Quaternion.Euler(pitch, yaw, 0f);
    }

    public void SetLookEnable(bool enabled)
    {
        this.enabled = enabled;
    }
}
