using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform carTransform;

    [Header("Camera Position")]
    public float distance = 8f;
    public float height = 4.5f;
    public float sideOffset = 2f;

    [Header("Follow")]
    public float followSpeed = 8f;
    public float lookSpeed = 8f;

    void LateUpdate()
    {
        if (carTransform == null)
            return;

        // Position behind and slightly to the side of the car
        Vector3 targetPosition =
            carTransform.position
            - carTransform.forward * distance
            + carTransform.up * height
            + carTransform.right * sideOffset;

        // Smooth camera movement
        transform.position = Vector3.Lerp(
            transform.position,
            targetPosition,
            followSpeed * Time.deltaTime
        );

        // Look slightly above the car
        Vector3 lookTarget =
            carTransform.position +
            carTransform.up * 1.2f;

        Quaternion targetRotation =
            Quaternion.LookRotation(
                lookTarget - transform.position
            );

        transform.rotation = Quaternion.Slerp(
            transform.rotation,
            targetRotation,
            lookSpeed * Time.deltaTime
        );
    }
}