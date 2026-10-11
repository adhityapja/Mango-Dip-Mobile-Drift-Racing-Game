using UnityEngine;
using TMPro;

public class VillainPointer : MonoBehaviour
{
    public Transform playerCar;
    public Transform villain;
    public Camera playerCamera;

    [Header("Distance")]
    public float farDistance = 80f;
    public float nearDistance = 30f;

    [Header("Pointer")]
    public float screenRadius = 250f;

    [Header("Colors")]
    public Color farColor = Color.green;
    public Color midColor = Color.yellow;
    public Color nearColor = Color.red;

    private TextMeshProUGUI pointerText;
    private RectTransform pointerRect;

    void Start()
    {
        pointerText = GetComponent<TextMeshProUGUI>();
        pointerRect = GetComponent<RectTransform>();

        if (playerCamera == null)
            playerCamera = Camera.main;
    }

    void Update()
    {
        if (playerCar == null || villain == null || playerCamera == null)
            return;

        Vector3 direction =
            villain.position - playerCar.position;

        float distance = direction.magnitude;

        // -------------------------
        // COLOR
        // -------------------------

        if (distance > farDistance)
        {
            pointerText.color = farColor;
        }
        else if (distance > nearDistance)
        {
            pointerText.color = midColor;
        }
        else
        {
            pointerText.color = nearColor;
        }

        // -------------------------
        // DIRECTION
        // -------------------------

        Vector3 localDirection =
            playerCamera.transform.InverseTransformDirection(
                direction.normalized
            );

        float angle =
            Mathf.Atan2(
                localDirection.x,
                localDirection.z
            ) * Mathf.Rad2Deg;

        pointerRect.localRotation =
            Quaternion.Euler(0f, 0f, -angle);

        // -------------------------
        // SCREEN POSITION
        // -------------------------

        Vector2 screenDirection =
            new Vector2(
                localDirection.x,
                localDirection.z
            );

        if (screenDirection.sqrMagnitude < 0.01f)
            screenDirection = Vector2.up;

        screenDirection.Normalize();

        pointerRect.anchoredPosition =
            screenDirection * screenRadius;
    }
}