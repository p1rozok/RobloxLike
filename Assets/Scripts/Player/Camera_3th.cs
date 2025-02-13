using UnityEngine;

public class ThirdPersonCamera : MonoBehaviour
{
    [Header("Camera Settings")]
    [SerializeField] private Transform player; 
    [SerializeField] private float sensitivity = 3f;
    [SerializeField] private float rotationSmoothSpeed = 10f;
    [SerializeField] private float minPitch = -30f;
    [SerializeField] private float maxPitch = 60f;

    private float yaw, pitch = 10f;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        yaw = transform.localEulerAngles.y;
    }

    private void LateUpdate()
    {
        yaw += Input.GetAxis("Mouse X") * sensitivity;
        pitch -= Input.GetAxis("Mouse Y") * sensitivity;
        pitch = Mathf.Clamp(pitch, minPitch, maxPitch);
        
        transform.localRotation = Quaternion.Euler(pitch, 0, 0);
        
        Quaternion targetPlayerRotation = Quaternion.Euler(0, yaw, 0);
        player.rotation = Quaternion.Slerp(player.rotation, targetPlayerRotation, Time.deltaTime * rotationSmoothSpeed);
    }
}