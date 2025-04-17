using UnityEngine;

public class LazerSwitch : MonoBehaviour
{
    public string laserObjectName = "LaserBeam(Clone)";
    public float interactionRange = 3f;
    public Transform interactionOrigin;

    private LazerBeam laserToActivate;

    private void Start()
    {
        AssignPlayer();
        FindLaser();
    }

    private void Update()
    {
        if (interactionOrigin == null)
        {
            AssignPlayer();
        }

        if (laserToActivate == null)
        {
            FindLaser();
        }

        if (Input.GetKeyDown(KeyCode.F))
        {
            TryActivateLaser();
        }
    }

    void AssignPlayer()
    {
        GameObject player = GameObject.Find("Player");
        if (player != null)
        {
            interactionOrigin = player.transform;
            Debug.Log("Player assigned.");
        }
        else
        {
            Debug.LogWarning(" Player not found.");
        }
    }

    void FindLaser()
    {
        GameObject laserObj = GameObject.Find(laserObjectName);
        if (laserObj != null)
        {
            laserToActivate = laserObj.GetComponent<LazerBeam>();
            Debug.Log("Laser assigned.");
        }
        else
        {
            Debug.LogWarning(" Laser not found.");
        }
    }

    void TryActivateLaser()
    {
        if (laserToActivate != null && interactionOrigin != null)
        {
            float distance = Vector3.Distance(interactionOrigin.position, laserToActivate.transform.position);
            if (distance <= interactionRange)
            {
                laserToActivate.ActivateLaser();
                Debug.Log(" Laser activated.");
            }
            else
            {
                Debug.Log(" Too far from laser.");
            }
        }
        else
        {
            Debug.LogError(" Missing laser or player reference.");
        }
    }
}
