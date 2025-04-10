using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LazerBeam : MonoBehaviour
{
    [Header("Laser Settings")]
    public LineRenderer lineRenderer;
    public float laserLength = 10f;
    public int maxReflections = 10;
    public string reflectorTag = "Reflector";
    public string refractorTag = "Refractor";
    public string lazerEnd = "LazerEnd";
    public bool hitFinish;

    public Gradient laserGradient; 

    [SerializeField] private bool isLaserActive = false;

    void Start()
    {
        if (lineRenderer == null)
        {
            lineRenderer = GetComponent<LineRenderer>();
        }

        // Disable laser on start
        lineRenderer.enabled = false;
    }

    void Update()
    {
        if (isLaserActive)
        {
            ShootLaser();
        }
    }

    public void ActivateLaser()
    {
        if (!isLaserActive)
        {
            StartCoroutine(LaserRoutine());
        }
    }

    IEnumerator LaserRoutine()
    {
        isLaserActive = true;
        lineRenderer.enabled = true;

        float duration = Random.Range(10f, 20f);
        yield return new WaitForSeconds(duration);

        // Begin gradual fade
        yield return StartCoroutine(FadeOutLaser(2f)); // 2 seconds fade
        isLaserActive = false;
        lineRenderer.enabled = false;
    }


    IEnumerator FadeOutLaser(float fadeDuration)
    {
        float timer = 0f;
        
        GradientColorKey[] colorKeys = lineRenderer.colorGradient.colorKeys;
        GradientAlphaKey[] originalAlpha = lineRenderer.colorGradient.alphaKeys;

        // Start from full alpha
        float startAlpha = originalAlpha[0].alpha;

        while (timer < fadeDuration)
        {
            float t = timer / fadeDuration;
            float newAlpha = Mathf.Lerp(startAlpha, 0f, t);

            GradientAlphaKey[] newAlphaKeys = new GradientAlphaKey[]
            {
                new GradientAlphaKey(newAlpha, 0f),
                new GradientAlphaKey(newAlpha, 1f)
            };

            Gradient newGradient = new Gradient();
            newGradient.SetKeys(colorKeys, newAlphaKeys);
            lineRenderer.colorGradient = newGradient;

            timer += Time.deltaTime;
            yield return null;
        }

        // Fully transparent at the end
        GradientAlphaKey[] zeroAlphaKeys = new GradientAlphaKey[]
        {
            new GradientAlphaKey(0f, 0f),
            new GradientAlphaKey(0f, 1f)
        };

        Gradient finalGradient = new Gradient();
        finalGradient.SetKeys(colorKeys, zeroAlphaKeys);
        lineRenderer.colorGradient = finalGradient;
    }


    void ShootLaser()
    {
        Vector3 laserStart = transform.position;
        Vector3 laserDirection = transform.forward;
        List<Vector3> points = new List<Vector3> { laserStart };

        for (int i = 0; i < maxReflections; i++)
        {
            Ray ray = new Ray(laserStart, laserDirection);
            if (Physics.Raycast(ray, out RaycastHit hit, laserLength))
            {
                points.Add(hit.point);

                if (hit.collider.CompareTag(reflectorTag)) // reflection
                {
                    laserDirection = Vector3.Reflect(laserDirection, hit.normal);
                    laserStart = hit.point;
                }
                else if (hit.collider.CompareTag(refractorTag)) // refraction
                {
                    RefractionObj refractiveObject = hit.collider.GetComponent<RefractionObj>();
                    if (refractiveObject != null)
                    {
                        Vector3 refractedDirection;
                        if (refractiveObject.RefractLaser(laserDirection, hit.normal, out refractedDirection))
                        {
                            laserDirection = refractedDirection;
                        }
                        else
                        {
                            laserDirection = Vector3.Reflect(laserDirection, hit.normal);
                        }
                        laserStart = hit.point;
                    }
                }
                else if (hit.collider.CompareTag(lazerEnd))
                {
                    hitFinish = true;
                    Debug.Log("Lazer Finish");
                    break;
                }
                else
                {
                    break;
                }
            }
            else
            {
                points.Add(laserStart + laserDirection * laserLength);
                hitFinish = false;
                Debug.Log("Lazer did not hit Finish");
                break;
            }
        }

        lineRenderer.positionCount = points.Count;
        lineRenderer.SetPositions(points.ToArray());
    }
}
