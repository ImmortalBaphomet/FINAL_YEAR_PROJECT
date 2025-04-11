using System.Collections;
using UnityEngine;

public class LazerSwitch : MonoBehaviour
{
    public KeyCode activateKey = KeyCode.F;
    public LazerBeam laserToActivate;

    private bool playerInRange = false;
    private bool isGlowing = false;

    public Renderer parentRenderer; // Assign the parent's Renderer in Inspector
    public Color glowColor = Color.cyan;
    public float glowSpeed = 2f;
    public float maxGlowIntensity = 2f;

    private Material parentMaterial;
    private Coroutine glowCoroutine;

    void Start()
    {
        if (parentRenderer != null)
        {
            parentMaterial = parentRenderer.material;
            parentMaterial.EnableKeyword("_EMISSION");
            glowCoroutine = StartCoroutine(GlowLoop());
        }
    }

    void Update()
    {
        if (playerInRange && Input.GetKeyDown(activateKey))
        {
            if (laserToActivate != null)
            {
                laserToActivate.ActivateLaser();
                Debug.Log("Laser activated!");
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = true;
            Debug.Log("Player entered switch zone.");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;
            Debug.Log("Player left switch zone.");
        }
    }

    IEnumerator GlowLoop()
    {
        float t = 0f;
        while (true)
        {
            if (!playerInRange)
            {
                t += Time.deltaTime * glowSpeed;
                float emission = (Mathf.Sin(t) + 1f) / 2f * maxGlowIntensity;
                Color finalColor = glowColor * emission;
                parentMaterial.SetColor("_EmissionColor", finalColor);
            }
            else
            {
                // Stop glowing
                parentMaterial.SetColor("_EmissionColor", Color.black);
            }
            yield return null;
        }
    }
}
