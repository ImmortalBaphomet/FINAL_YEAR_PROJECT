using UnityEngine;

public class CamChangeTrigger : MonoBehaviour
{
    public GameObject sideCam;
    public GameObject isoCam;

    private void Awake()
    {
        if (sideCam == null && Camera.main != null)
        {
            sideCam = Camera.main.gameObject;
        }

        // Just in case only one camera is active at start
        if (sideCam.activeSelf == isoCam.activeSelf)
        {
            sideCam.SetActive(true);
            isoCam.SetActive(false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Toggle camera based on current active state
            bool sideCamActive = sideCam.activeSelf;
            bool isoCamActive = isoCam.activeSelf;

            if (sideCam != null && isoCam != null)
            {
                sideCam.SetActive(!sideCamActive);
                isoCam.SetActive(!isoCamActive);
            }
            else
            {
                Debug.LogWarning("Cameras not assigned in the Inspector!");
            }
        }
    }
}
