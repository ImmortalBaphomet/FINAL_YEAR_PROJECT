using System.Collections;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class LightSpeedDash : MonoBehaviour
{
    [Header("Dash Settings")]
    [SerializeField] private float dashDistance = 10f;
    [SerializeField] private float dashCooldown = 2f;
    [SerializeField] private float dashDuration = 0.2f;
    [SerializeField] private KeyCode dashKey = KeyCode.LeftShift;

    [Header("Trail Effect")]
    [SerializeField] private TrailRenderer dashTrail;
    [SerializeField] private GameObject dashTrailHolder;
    [SerializeField] private Renderer playerRenderer;

    [Header("Collision Settings")]
    [SerializeField] private LayerMask obstacleLayers;
    [SerializeField] private float dashCollisionRadius = 0.5f;

    private CharacterController controller;
    private bool canDash = true;
    private Vector3 dashDirection;

    private void Awake()
    {
        controller = GetComponent<CharacterController>();

        if (dashTrailHolder) dashTrailHolder.SetActive(false);
        if (dashTrail) dashTrail.enabled = false;
    }

    private void Update()
    {
        if (canDash && Input.GetKeyDown(dashKey))
        {
            dashDirection = Input.GetKey(KeyCode.Space)
                ? (transform.forward + Vector3.up).normalized
                : transform.forward.normalized;

            StartCoroutine(DashRoutine());
        }
    }


    private IEnumerator DashRoutine()
    {
        canDash = false;
        float dashSpeed = dashDistance / dashDuration;
        float elapsed = 0f;

        ToggleVisuals(false);

        while (elapsed < dashDuration)
        {
            float step = dashSpeed * Time.deltaTime;
            controller.Move(dashDirection * step); // Let CharacterController handle collisions

            elapsed += Time.deltaTime;
            yield return null;
        }

        ToggleVisuals(true);
        yield return new WaitForSeconds(dashCooldown);
        canDash = true;
    }


    private void ToggleVisuals(bool visible)
    {
        if (playerRenderer) playerRenderer.enabled = visible;

        if (dashTrail)
        {
            dashTrail.enabled = !visible;
            if (visible) dashTrail.Clear();
        }

        if (dashTrailHolder) dashTrailHolder.SetActive(!visible);
    }
}

/*
using System.Collections;
using UnityEngine;

public class LightSpeedDash : MonoBehaviour
{
    

    void Start()
    {
        controller = GetComponent<CharacterController>();
        dashTrailHolder?.SetActive(false);
    }

    void Update()
    {
        if (canDash && Input.GetKeyDown(dashKey))
        {
            StartCoroutine(PerformDash());
        }
    }

    private Vector3 CalculateDashDirection()
    {
        return Input.GetKey(KeyCode.Space)
            ? (transform.forward + Vector3.up).normalized
            : transform.forward.normalized;
    }

    private IEnumerator PerformDash()
    {
        canDash = false;
        dashDirection = CalculateDashDirection();
        float dashSpeed = dashDistance / dashDuration;
        float elapsedTime = 0f;

        // Hide visuals and enable trail
        playerRenderer.enabled = false;
        if (dashTrail != null)
        {
            dashTrail.enabled = true;
            dashTrailHolder?.SetActive(true);
        }

        while (elapsedTime < dashDuration)
        {
            float stepDistance = dashSpeed * Time.deltaTime;

            // Obstacle check
            if (Physics.SphereCast(transform.position, dashCollisionRadius, dashDirection, out RaycastHit hit, stepDistance, obstacleLayers))
            {
                break; // Hit obstacle – stop dash
            }

            controller.Move(dashDirection * stepDistance);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Restore visuals and disable trail
        playerRenderer.enabled = true;
        if (dashTrail != null)
        {
            dashTrail.enabled = false;
            dashTrailHolder?.SetActive(false);
            dashTrail.Clear();
        }

        yield return new WaitForSeconds(dashCooldown);
        canDash = true;
    }
}

*/