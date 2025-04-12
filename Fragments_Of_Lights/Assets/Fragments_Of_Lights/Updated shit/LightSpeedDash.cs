using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]
public class LightSpeedDash : MonoBehaviour
{
    [Header("Dash Settings")]
    [SerializeField] private float dashDistance = 10f;
    [SerializeField] private float dashCooldown = 2f;
    [SerializeField] private float dashDuration = 0.2f;

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
    private PlayerInput playerInput;
    private InputAction dashAction;
    private InputAction jumpAction;

    private void Awake()
    {
        controller = GetComponent<CharacterController>();

        if (dashTrailHolder) dashTrailHolder.SetActive(false);
        if (dashTrail) dashTrail.enabled = false;
    }

    private void OnEnable()
    {
        playerInput = GetComponent<PlayerInput>();

        if (playerInput != null)
        {
            dashAction = playerInput.actions["Dash"];
            jumpAction = playerInput.actions["Jump"];

            dashAction.performed += onDash;
            dashAction.Enable();
            jumpAction.Enable();
        }
    }

    private void OnDisable()
    {
        if (dashAction != null) dashAction.performed -= onDash;
    }

    public void onDash(InputAction.CallbackContext context)
    {
        if (!canDash) return;

        // Optional: Use jump key to modify dash direction
        bool isJumping = jumpAction != null && jumpAction.IsPressed();
        dashDirection = isJumping
            ? (transform.forward + Vector3.up).normalized
            : transform.forward.normalized;

        StartCoroutine(DashRoutine());
    }

    private IEnumerator DashRoutine()
    {
        canDash = false;
        float dashSpeed = dashDistance / dashDuration;
        float elapsed = 0f;

        ToggleVisuals(false);
        AudioManager.instance.PlayClip(AudioManager.instance.dashAudio, false, 1f);

        while (elapsed < dashDuration)
        {
            float step = dashSpeed * Time.deltaTime;

            // Optional: Check for obstacles during dash
            if (Physics.SphereCast(transform.position, dashCollisionRadius, dashDirection, out RaycastHit hit, step, obstacleLayers))
            {
                break; // Stop dash on collision
            }

            controller.Move(dashDirection * step);
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
