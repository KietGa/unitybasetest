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

    [Header("Camera Collision")]
    [SerializeField] private LayerMask collisionMask;
    [SerializeField] private float cameraRadius = 0.2f;
    [SerializeField] private float collisionOffset = 0.1f;
    [SerializeField] private float cameraSmooth = 12f;

    private float yaw;
    private float pitch = 20f;
    private float currentDistance;

    private void Awake()
    {
        currentDistance = distance;
    }

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

        Quaternion rotation = Quaternion.Euler(pitch, yaw, 0f);

        Vector3 targetPosition = target.position + Vector3.up * height;
        Vector3 direction = -(rotation * Vector3.forward);

        float targetDistance = distance;

        if (Physics.SphereCast(
            targetPosition,
            cameraRadius,
            direction,
            out RaycastHit hit,
            distance,
            collisionMask,
            QueryTriggerInteraction.Ignore))
        {
            targetDistance = Mathf.Max(0f, hit.distance - collisionOffset);
        }

        if (targetDistance < currentDistance)
        {
            currentDistance = targetDistance;
        }
        else
        {
            currentDistance = Mathf.Lerp(
                currentDistance,
                targetDistance,
                cameraSmooth * Time.deltaTime
            );
        }

        transform.position = targetPosition + direction * currentDistance;
        transform.rotation = rotation;
    }
}