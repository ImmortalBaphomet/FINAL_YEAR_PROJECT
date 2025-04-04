using System.Collections;
using UnityEngine;

public class LightSpeedDash : MonoBehaviour
{
    [Header("Dash Settings")]
    [SerializeField] private float dashDistance = 10f;
    [SerializeField] private float dashCooldown = 2f;
    [SerializeField] private float dashDuration = 0.2f;
    [SerializeField] private KeyCode dashKey;

    [Header("Trail Effect")]
    [SerializeField] private TrailRenderer dashTrail;
    [SerializeField] private GameObject dashTrailHolder;
    [SerializeField] private Renderer playerRenderer;

    [Header("Collision Settings")]
    [SerializeField] private LayerMask obstacleLayers;  // Everything except "Trap"
    [SerializeField] private float dashCollisionRadius = 0.5f;  // Adjust based on player size

    private bool canDash = true;
    private Vector3 dashDirection;
    private CharacterController controller;

    void Start()
    {
        canDash = true;
        dashTrailHolder.SetActive(false);
        controller = GetComponent<CharacterController>(); // Ensure CharacterController is attached
    }

    void Update()
    {
        if (Input.GetKeyDown(dashKey) && canDash)
        {
            Dash();
        }
    }

    private void Dash()
    {
        if (!canDash) return;

        // Calculate direction
        if (Input.GetKey(KeyCode.Space))
        {
            dashDirection = (transform.forward + Vector3.up).normalized;
        }
        else
        {
            dashDirection = transform.forward.normalized;
        }

        StartCoroutine(PerformDash());
    }

    private IEnumerator PerformDash()
    {
        canDash = false;
        playerRenderer.enabled = false;

        if (dashTrail != null)
        {
            dashTrail.enabled = true;
            dashTrailHolder.SetActive(true);
        }

        float elapsedTime = 0f;
        float dashSpeed = dashDistance / dashDuration;

        while (elapsedTime < dashDuration)
        {
            float moveStep = dashSpeed * Time.deltaTime;
            Vector3 nextPosition = transform.position + dashDirection * moveStep;

            // **SphereCast to check for obstacles mid-dash**
            if (Physics.SphereCast(transform.position, dashCollisionRadius, dashDirection, out RaycastHit hit, moveStep, obstacleLayers))
            {
                // Stop at the point of collision
                nextPosition = hit.point;
                break;
            }

            // Move with CharacterController
            controller.Move(dashDirection * moveStep);

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        playerRenderer.enabled = true;
        if (dashTrail != null)
        {
            dashTrail.enabled = false;
            dashTrailHolder.SetActive(false);
            dashTrail.Clear();
        }

        yield return new WaitForSeconds(dashCooldown);
        canDash = true;
    }
}
