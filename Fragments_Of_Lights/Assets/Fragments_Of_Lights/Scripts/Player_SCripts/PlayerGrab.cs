using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerGrab : MonoBehaviour
{
    [Header("Input System Actions")]
    private InputActionReference grabAction;
    private InputActionReference rotateAction;

    [Header("Keyboard Fallback (Optional)")]
    public KeyCode grabKey = KeyCode.F;
    public KeyCode rotateClockwiseKey = KeyCode.E;
    public KeyCode rotateCounterClockwiseKey = KeyCode.Q;

    [Header("Grab Settings")]
    public float rotationSpeed = 100f;
    public float grabRange = 2f;
    public Transform interactionOrigin;

    public bool isGrabbing = false;
    private Transform grabbedObject;
    private Transform originalParent;
    private Animator anim;
    private int grabLayerIndex = 1;

    private float currentRotationInput = 0f;

    void OnEnable()
    {
        if (grabAction != null)
            grabAction.action.performed += OnGrab;

        if (rotateAction != null)
        {
            rotateAction.action.performed += OnRotatePerformed;
            rotateAction.action.canceled += OnRotateCanceled;
            rotateAction.action.Enable();
        }
    }

    void OnDisable()
    {
        if (grabAction != null)
            grabAction.action.performed -= OnGrab;

        if (rotateAction != null)
        {
            rotateAction.action.performed -= OnRotatePerformed;
            rotateAction.action.canceled -= OnRotateCanceled;
            rotateAction.action.Disable();
        }
    }

    void Start()
    {
        anim = GetComponent<Animator>();
        originalParent = transform.parent;
    }

    void Update()
    {
        // Fallback for keyboard grabbing
        if (Input.GetKeyDown(grabKey))
        {
            HandleGrab();
        }

        // Keyboard fallback rotation
        if (isGrabbing && grabbedObject != null)
        {
            float rotation = 0;
            if (Input.GetKey(rotateClockwiseKey))
                rotation = rotationSpeed * Time.deltaTime;
            else if (Input.GetKey(rotateCounterClockwiseKey))
                rotation = -rotationSpeed * Time.deltaTime;

            if (rotation != 0)
                grabbedObject.Rotate(Vector3.up * rotation);
        }

        // Gamepad rotation input
        if (isGrabbing && grabbedObject != null && Mathf.Abs(currentRotationInput) > 0.01f)
        {
            float rotation = currentRotationInput * rotationSpeed * Time.deltaTime;
            grabbedObject.Rotate(Vector3.up * rotation);
        }
    }

    public void OnGrab(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            HandleGrab();
        }
    }

   public void OnRotatePerformed(InputAction.CallbackContext context)
    {
        currentRotationInput = context.ReadValue<float>();
    }


    public void OnRotateCanceled(InputAction.CallbackContext context)
    {
        currentRotationInput = 0f;
    }

    void HandleGrab()
    {
        RaycastHit hit;
        Vector3 origin = interactionOrigin ? interactionOrigin.position : transform.position + Vector3.up * 1f;

        if (Physics.Raycast(origin, transform.forward, out hit, grabRange))
        {
            // Check for Door
            Door door = hit.collider.GetComponent<Door>();
            if (door != null)
            {
                //door.Interact();  // Calls the new method in Door.cs
                return;
            }

            // Check for Grabbable
            if (hit.collider.CompareTag("Grabbable"))
            {
                grabbedObject = hit.collider.transform;
                isGrabbing = true;

                transform.SetParent(grabbedObject);

                anim.SetLayerWeight(grabLayerIndex, 1);
                anim.SetBool("isGrab", true);
            }
        }
        else if (isGrabbing)
        {
            DropObject();
        }
    }


    // void HandleGrab()
    // {
    //     if (isGrabbing)
    //     {
    //         DropObject();
    //     }
    //     else
    //     {
    //         TryGrabObject();
    //     }
    // }



    void TryGrabObject()
    {
        RaycastHit hit;
        Vector3 origin = interactionOrigin ? interactionOrigin.position : transform.position + Vector3.up * 1f;

        if (Physics.Raycast(origin, transform.forward, out hit, grabRange))
        {
            if (hit.collider.CompareTag("Grabbable"))
            {
                grabbedObject = hit.collider.transform;
                isGrabbing = true;

                transform.SetParent(grabbedObject);

                anim.SetLayerWeight(grabLayerIndex, 1);
                anim.SetBool("isGrab", true);
            }
        }
    }

    void DropObject()
    {
        isGrabbing = false;
        grabbedObject = null;

        transform.SetParent(originalParent);

        anim.SetLayerWeight(grabLayerIndex, 0);
        anim.SetBool("isGrab", false);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Grabbable"))
        {
            Debug.Log("Can Grab Object");
        }
    }
}
