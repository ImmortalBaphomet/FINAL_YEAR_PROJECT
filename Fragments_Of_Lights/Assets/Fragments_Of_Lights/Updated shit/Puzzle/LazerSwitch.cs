using UnityEngine;
using UnityEngine.InputSystem;

public class LazerSwitch : MonoBehaviour
{
    [Header("Laser Settings")]
    public string laserObjectName = "LaserBeam(Clone)";
    public string laserTag = "Laser";
    private LazerBeam laserToActivate;

    [Header("Interaction Settings")]
    public float interactionRange = 3f;
    public Transform interactionOrigin;

    [Header("Input")]
    public InputActionReference activateAction; // <-- made public

    private void OnEnable()
    {
        if (activateAction != null)
            activateAction.action.performed += OnActivateLaser;
    }

    private void OnDisable()
    {
        if (activateAction != null)
            activateAction.action.performed -= OnActivateLaser;
    }

    private void Update()
    {
        if (laserToActivate == null)
        {
            FindLaser();
        }
    }

    private void FindLaser()
    {
        GameObject laserObj = GameObject.Find(laserObjectName);

        if (laserObj != null)
        {
            laserToActivate = laserObj.GetComponent<LazerBeam>();
            Debug.Log("Laser found and ready to activate.");
        }
    }

    public void OnActivateLaser(InputAction.CallbackContext context)
    {
        if (laserToActivate != null)
        {
            float distance = Vector3.Distance(
                interactionOrigin != null ? interactionOrigin.position : transform.position,
                laserToActivate.transform.position
            );

            if (distance <= interactionRange)
            {
                laserToActivate.ActivateLaser();
                Debug.Log("Laser activated by player input.");
            }
            else
            {
                Debug.Log("Too far from the laser to activate.");
            }
        }
        else
        {
            Debug.Log("No laser found to activate.");
        }
    }
}



//old logic

/*

[Header("Laser Settings")]
    public string laserObjectName = "LaserBeam(Clone)"; // name of the laser prefab when instantiated
    public string laserTag = "Laser"; // optional tag if you want to use tags instead
    private LazerBeam laserToActivate;

    [Header("Interaction Settings")]
    public float interactionRange = 3f;
    public Transform interactionOrigin;

    [Header("Input")]
    private InputActionReference activateAction;

    private void OnEnable()
    {
        if (activateAction != null)
        {
            activateAction.action.performed += OnActivateLaser;
            activateAction.action.Enable();
        }
    }

    private void OnDisable()
    {
        if (activateAction != null)
        {
            activateAction.action.performed -= OnActivateLaser;
            activateAction.action.Disable();
        }
    }

    private void Update()
    {
        // Always check if the laser exists
        if (laserToActivate == null)
        {
            FindLaser();
        }
    }

    private void FindLaser()
    {
        // You can choose to use either method: by name or by tag
        GameObject laserObj = GameObject.Find(laserObjectName);

        // Or use GameObject.FindWithTag("Laser") if you assigned a tag
        // GameObject laserObj = GameObject.FindWithTag(laserTag);

        if (laserObj != null)
        {
            laserToActivate = laserObj.GetComponent<LazerBeam>();
            Debug.Log("Laser found and ready to activate.");
        }
    }

    public void OnActivateLaser(InputAction.CallbackContext context)
    {
        if (laserToActivate != null)
        {
            float distance = Vector3.Distance(
                interactionOrigin != null ? interactionOrigin.position : transform.position,
                laserToActivate.transform.position
            );

            if (distance <= interactionRange)
            {
                laserToActivate.ActivateLaser();
                Debug.Log("Laser activated by player input.");
            }
            else
            {
                Debug.Log("Too far from the laser to activate.");
            }
        }
        else
        {
            Debug.Log("No laser found to activate.");
        }
    }

*/
