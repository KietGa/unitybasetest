
using UnityEngine;

public class ThirdPersonCamera : MonoBehaviour
{
    private const string MOUSEX = "Mouse X";
    private const string MOUSEY = "Mouse Y";

    [SerializeField] private Transform target;
    [SerializeField] private float distance = 5f;
    [SerializeField] private float height = 1.5f;
    [SerializeField] private float sensitivity = 3f;

    [SerializeField] private float minPitch = -30f;
    [SerializeField] private float maxPitch = 70f;

    private float yaw;
    private float pitch = 20f;

    private void LateUpdate()
    {
        RotateCamera();
        FollowTarget();
    }

    private void RotateCamera()
    {
        if (Input.GetMouseButton(0))
        {
            yaw += Input.GetAxis(MOUSEX) * sensitivity;
            pitch -= Input.GetAxis(MOUSEY) * sensitivity;

            pitch = Mathf.Clamp(pitch, minPitch, maxPitch);
        }
    }

    private void FollowTarget()
    {
        if (target == null) return;

        Quaternion rotation = Quaternion.Euler(pitch, yaw, 0);
        Vector3 targetPosition = target.position + Vector3.up * height;
        transform.position = targetPosition - rotation * Vector3.forward * distance;
        transform.rotation = rotation;
    }
}
