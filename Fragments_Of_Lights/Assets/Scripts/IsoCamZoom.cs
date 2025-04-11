using UnityEngine;

[RequireComponent(typeof(Camera))]
public class IsoCamZoom : MonoBehaviour
{
    public Transform player;
    public float minFOV = 40f;              // Zoom in
    public float maxFOV = 60f;              // Zoom out
    public float maxDistance = 30f;         // Max distance where minFOV applies
    public float zoomSpeed = 5f;            // FOV lerp speed
    public float rotateSpeed = 5f;          // Look-at rotation speed

    private Camera cam;

    void Awake()
    {
        cam = GetComponent<Camera>();

        if (player == null)
        {
            GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
            if (playerObj != null)
                player = playerObj.transform;
        }
    }

    void LateUpdate()
    {
        if (player == null || cam == null) return;

        // Smoothly rotate to look at the player
        Vector3 directionToPlayer = player.position - transform.position;
        Quaternion targetRotation = Quaternion.LookRotation(directionToPlayer, Vector3.up);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * rotateSpeed);

        // Adjust FOV based on distance to player (but don't move camera)
        float distance = directionToPlayer.magnitude;
        float t = Mathf.Clamp01(distance / maxDistance);
        float targetFOV = Mathf.Lerp(minFOV, maxFOV, 1 - t);
        cam.fieldOfView = Mathf.Lerp(cam.fieldOfView, targetFOV, Time.deltaTime * zoomSpeed);
    }
}
