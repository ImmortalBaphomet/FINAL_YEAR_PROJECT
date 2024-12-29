using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightScan : MonoBehaviour
{
    [SerializeField] ParticleSystem scan;
    [SerializeField] private KeyCode scanKey;
    public List<ParticleCollisionEvent> scanCollision;
    // Start is called before the first frame update
    void Start()
    {
        scan.Stop();
        //scan = GetComponentInChildren<ParticleSystem>();
        scanCollision = new List<ParticleCollisionEvent>();
    }

    // Update is called once per frame
    void Update()
    {
        ApplyScan();
    }
    void ApplyScan()
    {
        if (Input.GetKeyDown(scanKey))
        {
            scan.Play();

        }
        else
        {
            scan.Stop();
        }
    }

    void OnParticleCollision(GameObject other)
    {
        int numScannedObjects = scan.GetCollisionEvents(other, scanCollision);

        int i = 0;
        while (i < numScannedObjects)
        {
            Debug.Log("Scanned " + numScannedObjects + " objects");
            i++;
        }
    }
}