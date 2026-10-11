using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class VillainChase : MonoBehaviour
{
    public Transform target;

    [Header("References")]
    public NavMeshAgent agent;
    public Collider attackHitbox;

    [Header("Distance Settings")]
    public float farDistance = 80f;
    public float nearDistance = 30f;

    [Header("Speed")]
    public float minimumSpeed = 8f;
    public float maximumSpeed = 25f;

    [Header("Attack")]
    public float attackDistance = 12f;
    public float attackCooldown = 4f;
    public float jumpDuration = 1.2f;
    public float jumpHeight = 4f;
    public float jumpSpeed = 12f;

    [Header("Attack Hit")]
    public float attackHitDistance = 5f;

    [Header("Animation")]
    public float animationSpeedMultiplier = 0.5f;

    private Animator animator;
    private Rigidbody targetRigidbody;

    // Player health
    private PlayerHealth playerHealth;

    private bool isAttacking = false;
    private float attackTimer = 0f;

    void Start()
    {
        // Make sure attack hitbox starts OFF
        if (attackHitbox != null)
            attackHitbox.enabled = false;

        // Get NavMesh Agent
        if (agent == null)
            agent = GetComponent<NavMeshAgent>();

        // Get Animator
        animator = GetComponentInChildren<Animator>();

        // Get car Rigidbody
        if (target != null)
            targetRigidbody = target.GetComponent<Rigidbody>();

        // Get PlayerHealth
        if (target != null)
            playerHealth = target.GetComponent<PlayerHealth>();

        // Check references
        if (target == null)
            Debug.LogError("Villain target is not assigned!");

        if (agent == null)
            Debug.LogError("NavMeshAgent not found!");

        if (targetRigidbody == null)
            Debug.LogWarning("Car Rigidbody not found!");

        if (playerHealth == null)
            Debug.LogError("PlayerHealth not found on the car!");

        if (attackHitbox == null)
            Debug.LogError("Attack Hitbox is not assigned!");
    }

    void Update()
    {
        if (target == null || agent == null)
            return;

        // Attack cooldown
        if (attackTimer > 0f)
            attackTimer -= Time.deltaTime;

        // Distance between giant and car
        float distance =
            Vector3.Distance(
                transform.position,
                target.position
            );

        // Make sure giant is on NavMesh
        if (!agent.isOnNavMesh && !isAttacking)
            return;

        // =========================================
        // START ATTACK
        // =========================================

        if (!isAttacking &&
            distance <= attackDistance &&
            attackTimer <= 0f)
        {
            StartCoroutine(Attack());
            return;
        }

        // =========================================
        // DON'T CHASE DURING ATTACK
        // =========================================

        if (isAttacking)
            return;

        // =========================================
        // NORMAL CHASE
        // =========================================

        agent.isStopped = false;
        agent.updatePosition = true;

        // -----------------------------------------
        // GET CAR SPEED
        // -----------------------------------------

        float carSpeed = 0f;

        if (targetRigidbody != null)
        {
            carSpeed =
                targetRigidbody.velocity.magnitude;
        }

        // -----------------------------------------
        // CATCH-UP ADVANTAGE
        // -----------------------------------------

        float catchUpBoost;

        if (distance > farDistance)
        {
            // Very far away
            catchUpBoost = 5f;
        }
        else if (distance > nearDistance)
        {
            // Medium distance
            catchUpBoost = 3f;
        }
        else
        {
            // Close distance
            catchUpBoost = 1.5f;
        }

        // -----------------------------------------
        // GIANT SPEED
        // -----------------------------------------

        float desiredSpeed =
            carSpeed + catchUpBoost;

        desiredSpeed = Mathf.Clamp(
            desiredSpeed,
            minimumSpeed,
            maximumSpeed
        );

        agent.speed = desiredSpeed;

        // -----------------------------------------
        // FOLLOW CAR USING NAVMESH
        // -----------------------------------------

        agent.SetDestination(
            target.position
        );

        // -----------------------------------------
        // RUN ANIMATION
        // -----------------------------------------

        if (animator != null)
        {
            float movementSpeed =
                agent.velocity.magnitude;

            animator.SetFloat(
                "Speed",
                movementSpeed *
                animationSpeedMultiplier,
                0.1f,
                Time.deltaTime
            );
        }
    }

    // =============================================
    // JUMP ATTACK
    // =============================================

    IEnumerator Attack()
    {
        isAttacking = true;

        // Start cooldown immediately
        attackTimer = attackCooldown;

        // =========================================
        // ENABLE ATTACK HITBOX
        // =========================================

        if (attackHitbox != null)
            attackHitbox.enabled = true;

        // =========================================
        // STOP NAVMESH
        // =========================================

        agent.isStopped = true;
        agent.updatePosition = false;

        // =========================================
        // START JUMP ANIMATION
        // =========================================

        if (animator != null)
        {
            animator.SetFloat(
                "Speed",
                0f
            );

            animator.SetBool(
                "Jump",
                true
            );
        }

        // =========================================
        // FACE THE CAR
        // =========================================

        Vector3 direction =
            target.position -
            transform.position;

        direction.y = 0f;

        if (direction.sqrMagnitude > 0.01f)
        {
            transform.rotation =
                Quaternion.LookRotation(
                    direction
                );
        }

        // =========================================
        // START POSITION
        // =========================================

        Vector3 startPosition =
            transform.position;

        // =========================================
        // PREDICT CAR POSITION
        // =========================================

        Vector3 targetPosition =
            target.position;

        if (targetRigidbody != null)
        {
            targetPosition +=
                targetRigidbody.velocity *
                0.35f;
        }

        // Keep jump on same ground level
        targetPosition.y =
            startPosition.y;

        float elapsed = 0f;

        // IMPORTANT:
        // This allows ONLY ONE hit per jump.
        bool hitPlayer = false;

        // =========================================
        // JUMP LOOP
        // =========================================

        while (elapsed < jumpDuration)
        {
            elapsed += Time.deltaTime;

            float t =
                Mathf.Clamp01(
                    elapsed /
                    jumpDuration
                );

            // -------------------------------------
            // MOVE TOWARD CAR
            // -------------------------------------

            Vector3 horizontalPosition =
                Vector3.Lerp(
                    startPosition,
                    targetPosition,
                    t
                );

            // -------------------------------------
            // JUMP ARC
            // -------------------------------------

            float verticalOffset =
                Mathf.Sin(
                    t * Mathf.PI
                ) * jumpHeight;

            horizontalPosition.y =
                startPosition.y +
                verticalOffset;

            transform.position =
                horizontalPosition;

            // -------------------------------------
            // FACE CAR DURING JUMP
            // -------------------------------------

            Vector3 lookDirection =
                target.position -
                transform.position;

            lookDirection.y = 0f;

            if (lookDirection.sqrMagnitude > 0.01f)
            {
                Quaternion rotation =
                    Quaternion.LookRotation(
                        lookDirection
                    );

                transform.rotation =
                    Quaternion.Slerp(
                        transform.rotation,
                        rotation,
                        10f *
                        Time.deltaTime
                    );
            }

            // =====================================
            // CHECK ATTACK HIT
            // =====================================

            if (!hitPlayer &&
                attackHitbox != null)
            {
                Bounds hitboxBounds =
                    attackHitbox.bounds;

                Collider[] hitObjects =
                    Physics.OverlapBox(
                        hitboxBounds.center,
                        hitboxBounds.extents,
                        attackHitbox.transform.rotation
                    );

                foreach (Collider hitObject in hitObjects)
                {
                    // Check whether this collider
                    // belongs to the player's car
                    if (hitObject.transform == target ||
                        hitObject.transform.IsChildOf(target))
                    {
                        // =================================
                        // IMPORTANT:
                        // REGISTER HIT IMMEDIATELY
                        // =================================

                        hitPlayer = true;

                        Debug.Log(
                            "🔥 PLAYER HIT BY GIANT!"
                        );

                        // =================================
                        // REMOVE EXACTLY ONE LIFE
                        // =================================

                        if (playerHealth != null)
                        {
                            playerHealth.TakeDamage();

                            Debug.Log(
                                "💔 ONE LIFE LOST!"
                            );
                        }

                        // =================================
                        // DISABLE HITBOX IMMEDIATELY
                        // =================================

                        attackHitbox.enabled = false;

                        // Stop checking other colliders
                        break;
                    }
                }
            }

            yield return null;
        }

        // =========================================
        // LAND
        // =========================================

        if (animator != null)
        {
            animator.SetBool(
                "Jump",
                false
            );
        }

        // =========================================
        // MAKE SURE HITBOX IS OFF
        // =========================================

        if (attackHitbox != null)
            attackHitbox.enabled = false;

        // =========================================
        // FIND VALID NAVMESH POSITION
        // =========================================

        NavMeshHit navHit;

        if (NavMesh.SamplePosition(
            transform.position,
            out navHit,
            10f,
            NavMesh.AllAreas))
        {
            transform.position =
                navHit.position;
        }

        // =========================================
        // RESET NAVMESH AGENT
        // =========================================

        agent.Warp(
            transform.position
        );

        agent.updatePosition = true;
        agent.isStopped = false;

        // =========================================
        // RESET ATTACK STATE
        // =========================================

        isAttacking = false;

        // =========================================
        // ATTACK RESULT
        // =========================================

        if (hitPlayer)
        {
            Debug.Log(
                "💥 ATTACK SUCCESSFUL - 1 LIFE LOST!"
            );
        }
        else
        {
            Debug.Log(
                "🟢 ATTACK MISSED!"
            );
        }

        Debug.Log(
            "GIANT ATTACK FINISHED"
        );
    }
}