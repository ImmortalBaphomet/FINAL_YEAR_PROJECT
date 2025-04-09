using System.Collections;
using UnityEngine;

public class HKcam : MonoBehaviour
{
    public enum CameraMode { SideView, IsoView }
    [Header("Mode")]
    public CameraMode currentMode = CameraMode.SideView;

    [Header("Target Settings")]
    public Transform target;

    [Header("Camera Settings")]
    public float smoothSpeed = 0.125f;
    public Vector3 offset;

    [Header("Dead Zone Settings")]
    public bool useDeadZone = true;
    public Vector2 deadZoneSize = new Vector2(2f, 1.5f);

    [Header("Camera Boundaries")]
    public bool useBoundaries = false;
    public Vector2 minBounds;
    public Vector2 maxBounds;

    [Header("View Anchors")]
    public Transform sideViewAnchor;  // Empty GameObject that holds side-view position/rotation
    public Transform isoViewAnchor;   // Empty GameObject that holds isometric-view position/rotation
    public float transitionDuration = 2f;
    public AnimationCurve transitionCurve;

    private Vector3 velocity = Vector3.zero;
    private bool isTransitioning = false;
    public KeyCode camChange1;
    public KeyCode camChange2;


    void Update()
    {
        if (Input.GetKeyDown(camChange1))
            SwitchToSideView();
        if (Input.GetKeyDown(camChange2))
            SwitchToIsoView();
    }


    private void LateUpdate()
    {
        if (isTransitioning || target == null)
            return;

        if (currentMode == CameraMode.IsoView)
        {
            // Stay fixed at the isometric anchor position
            if (isoViewAnchor != null)
            {
                transform.position = isoViewAnchor.position;
                transform.rotation = isoViewAnchor.rotation;
            }
            return;
        }

        // Side View dynamic follow logic
        Vector3 desiredPosition = target.position + offset;

        if (useDeadZone)
        {
            Vector3 delta = desiredPosition - transform.position;

            if (Mathf.Abs(delta.x) > deadZoneSize.x / 2)
                desiredPosition.x = transform.position.x + delta.x - Mathf.Sign(delta.x) * (deadZoneSize.x / 2);

            if (Mathf.Abs(delta.y) > deadZoneSize.y / 2)
                desiredPosition.y = transform.position.y + delta.y - Mathf.Sign(delta.y) * (deadZoneSize.y / 2);
            else
                desiredPosition.y = transform.position.y;
        }

        Vector3 smoothedPosition = Vector3.SmoothDamp(transform.position, desiredPosition, ref velocity, smoothSpeed);

        if (useBoundaries)
        {
            smoothedPosition.x = Mathf.Clamp(smoothedPosition.x, minBounds.x, maxBounds.x);
            smoothedPosition.y = Mathf.Clamp(smoothedPosition.y, minBounds.y, maxBounds.y);
        }

        transform.position = new Vector3(smoothedPosition.x, smoothedPosition.y, transform.position.z);
    }


    // Call this to switch to isometric view
    public void SwitchToIsoView()
    {
        if (isoViewAnchor != null)
            StartCoroutine(SmoothCameraTransition(isoViewAnchor, CameraMode.IsoView));
    }

    // Call this to return to side-scroller view
    public void SwitchToSideView()
    {
        if (sideViewAnchor != null)
            StartCoroutine(SmoothCameraTransition(sideViewAnchor, CameraMode.SideView));
    }

    private IEnumerator SmoothCameraTransition(Transform targetAnchor, CameraMode nextMode)
    {
        isTransitioning = true;

        Vector3 startPos = transform.position;
        Quaternion startRot = transform.rotation;
        Vector3 endPos = targetAnchor.position;
        Quaternion endRot = targetAnchor.rotation;

        float elapsed = 0f;

        while (elapsed < transitionDuration)
        {
            float t = elapsed / transitionDuration;
            if (transitionCurve != null)
                t = transitionCurve.Evaluate(t);

            transform.position = Vector3.Lerp(startPos, endPos, t);
            transform.rotation = Quaternion.Slerp(startRot, endRot, t);

            elapsed += Time.deltaTime;
            yield return null;
        }

        transform.position = endPos;
        transform.rotation = endRot;

        currentMode = nextMode;
        isTransitioning = false;
    }
}
