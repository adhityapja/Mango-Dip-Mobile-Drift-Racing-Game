using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Movement")]
    public float walkSpeed = 3f;
    public float runSpeed = 7f;
    public float rotationSpeed = 10f;

    [Header("Jump")]
    public float jumpHeight = 2f;
    public float gravity = -9.81f;

    [Header("Camera")]
    public Transform cameraTransform;

    private CharacterController controller;
    private Animator animator;

    private Vector3 velocity;

    private bool isJumping = false;

    void Start()
    {
        controller = GetComponent<CharacterController>();

        // Animator is inside Fight Idle
        animator = GetComponentInChildren<Animator>();

        if (controller == null)
        {
            Debug.LogError("CharacterController is missing!");
        }

        if (animator == null)
        {
            Debug.LogError("Animator not found!");
        }
    }

    void Update()
    {
        // ==========================================
        // MOVEMENT INPUT
        // ==========================================

        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        // ==========================================
        // CAMERA RELATIVE MOVEMENT
        // ==========================================

        Vector3 forward = cameraTransform.forward;
        Vector3 right = cameraTransform.right;

        forward.y = 0f;
        right.y = 0f;

        forward.Normalize();
        right.Normalize();

        Vector3 movement =
            forward * vertical +
            right * horizontal;

        if (movement.magnitude > 1f)
        {
            movement.Normalize();
        }

        // ==========================================
        // WALK / RUN
        // ==========================================

        bool isMoving = movement.magnitude > 0.01f;
        bool isRunning =
            Input.GetKey(KeyCode.LeftShift) && isMoving;

        float currentSpeed =
            isRunning ? runSpeed : walkSpeed;

        controller.Move(
            movement * currentSpeed * Time.deltaTime
        );

        // ==========================================
        // ROTATION
        // ==========================================

        if (isMoving)
        {
            Quaternion targetRotation =
                Quaternion.LookRotation(movement);

            transform.rotation =
                Quaternion.Slerp(
                    transform.rotation,
                    targetRotation,
                    rotationSpeed * Time.deltaTime
                );
        }

        // ==========================================
        // MOVEMENT ANIMATION
        // ==========================================

        if (animator != null)
        {
            float animationSpeed = 0f;

            if (isMoving)
            {
                if (isRunning)
                {
                    animationSpeed = 5f;
                }
                else
                {
                    animationSpeed = 2f;
                }
            }

            animator.SetFloat(
                "Speed",
                animationSpeed,
                0.1f,
                Time.deltaTime
            );
        }

        // ==========================================
        // GROUND CHECK
        // ==========================================

        bool grounded = controller.isGrounded;

        if (grounded && velocity.y < 0f)
        {
            velocity.y = -2f;
        }

        // ==========================================
        // JUMP
        // ==========================================

        if (grounded &&
            !isJumping &&
            Input.GetKeyDown(KeyCode.Space))
        {
            velocity.y = Mathf.Sqrt(
                jumpHeight * -2f * gravity
            );

            isJumping = true;

            if (animator != null)
            {
                animator.SetBool("Jump", true);
            }
        }

        // ==========================================
        // GRAVITY
        // ==========================================

        velocity.y += gravity * Time.deltaTime;

        controller.Move(
            velocity * Time.deltaTime
        );

        // ==========================================
        // LANDING
        // ==========================================

        if (isJumping &&
            controller.isGrounded &&
            velocity.y < -1f)
        {
            isJumping = false;

            if (animator != null)
            {
                animator.SetBool("Jump", false);
            }
        }
    }
}