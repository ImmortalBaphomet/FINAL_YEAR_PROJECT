using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGrab : MonoBehaviour
{
    public float rotationSpeed = 100f;
    public KeyCode grabKey = KeyCode.F;
    public KeyCode rotateClockwiseKey = KeyCode.E;
    public KeyCode rotateCounterClockwiseKey = KeyCode.Q;
    
    public float reqSpeed;

    public bool isGrabbing = false;
    public bool canGrab = false;
    private Transform grabbedObject;
    private Animator anim;
    private int grabLayerIndex = 1; // Grab layer index
    private Transform originalParent; // Store original parent of the player
    
    void Start()
    {
        anim = GetComponent<Animator>();
        originalParent = transform.parent; // Save the original paren
    }

    void Update()
    {
        if (Input.GetKeyDown(grabKey))
        {
            if (isGrabbing)
            {
                DropObject();
            }
            else
            {
                TryGrabObject();
            }
        }

        if (isGrabbing && grabbedObject != null)
        {
            RotateObject();
        }
    }

    void TryGrabObject()
    {
        RaycastHit hit;
        Vector3 rayOrigin = transform.position + Vector3.up * 1f;
        if (Physics.Raycast(rayOrigin, transform.forward, out hit, 2f))
        {
            if (hit.collider.CompareTag("Grabbable"))
            {
                grabbedObject = hit.collider.transform;
                isGrabbing = true;

                // Set player as child of the grabbed object
                transform.SetParent(grabbedObject);
                // set movement speed = 0
                
                // *** Force player's scale to remain unchanged ***
                //transform.localScale = originalScale;

                anim.SetLayerWeight(grabLayerIndex, 1); // Activate upper body grab animation
                anim.SetBool("isGrab", true);
            }
        }
    }

    void DropObject()
    {
        isGrabbing = false;
        grabbedObject = null;

        // Reset player's parent and ensure scale stays the same
        transform.SetParent(originalParent);
        //transform.localScale = originalScale; // Restore scale after unparenting

        anim.SetLayerWeight(grabLayerIndex, 0); // Disable grab animation
        anim.SetBool("isGrab", false);
    }

    void RotateObject()
    {
        float rotation = 0;
        if (Input.GetKey(rotateClockwiseKey))
        {
            rotation = rotationSpeed * Time.deltaTime;
            AudioManager.instance.PlayClip(AudioManager.instance.mirrorRotateAudio, false, 1f);
        }
        else if (Input.GetKey(rotateCounterClockwiseKey))
        {
            rotation = -rotationSpeed * Time.deltaTime;
            AudioManager.instance.PlayClip(AudioManager.instance.mirrorRotateAudio, false, 1f);
        }

        grabbedObject.Rotate(Vector3.up * rotation);
    }
    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Grabbable")
        {
            Debug.Log("Can Grab Object");
            
        }
    }
}


