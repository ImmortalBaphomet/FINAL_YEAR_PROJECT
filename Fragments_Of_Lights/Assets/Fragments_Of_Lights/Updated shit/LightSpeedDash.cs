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
            Vector3 input = new Vector3(Input.GetAxisRaw("Horizontal"), 0f, Input.GetAxisRaw("Vertical")).normalized;
            dashDirection = input != Vector3.zero ? input : transform.forward;

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

            if (Physics.SphereCast(transform.position, dashCollisionRadius, dashDirection, out _, step, obstacleLayers))
                break;

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
