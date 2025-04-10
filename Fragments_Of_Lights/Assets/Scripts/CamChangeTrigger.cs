using UnityEngine;

public class CamChangeTrigger : MonoBehaviour
{
    public GameObject sideCam;
    public GameObject isoCam;

    private bool isIso = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isIso = !isIso;

            if (sideCam != null && isoCam != null)
            {
                sideCam.SetActive(!isIso);
                isoCam.SetActive(isIso);
            }
            else
            {
                Debug.LogWarning("Cameras not assigned in the Inspector!");
            }
        }
    }
}
