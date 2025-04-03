using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Roller : MonoBehaviour
{
    [Header("Cylinder Settings")]
    [SerializeField] private Transform cylinder;   // The rotating cylinder
    [SerializeField] private float rotationSpeed = 100f; // Speed of rotation

    [Header("Movement Settings")]
    [SerializeField] private float minY = 0f;  // Lowest Y position
    [SerializeField] private float maxY = 5f;  // Highest Y position
    [SerializeField] private float moveSpeed = 2f;  // Speed of movement

    private bool movingUp = true;

    void Start()
    {
            
    }

    void Update()
    {
        RotateRoller();
        MoveRod();
    }

    void RotateRoller()
    {
        if (cylinder != null)
        {
            cylinder.Rotate(Vector3.right * rotationSpeed * Time.deltaTime);
        }
    }
    
     private void MoveRod()
    {
        // Move towards the target position
        // Determine movement direction
        float targetY = movingUp ? maxY : minY;
        
        // Move towards the target Y position
        transform.position = Vector3.MoveTowards(transform.position,new Vector3(transform.position.x, targetY, transform.position.z),moveSpeed * Time.deltaTime);

        // Switch direction when reaching the limit
        if (Mathf.Abs(transform.position.y - targetY) < 0.01f)
        {
            movingUp = !movingUp;
        }
    }

}

/*
    using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RodMover : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField] private Transform startPoint;  // Start position
    [SerializeField] private Transform endPoint;    // End position
    [SerializeField] private float moveSpeed = 2f;  // Speed of movement

    private bool movingUp = true;

    void Update()
    {
        MoveRod();
    }

    private void MoveRod()
    {
        // Move towards the target position
        Transform target = movingUp ? endPoint : startPoint;
        transform.position = Vector3.MoveTowards(transform.position, target.position, moveSpeed * Time.deltaTime);

        // Check if the rod reached the target position
        if (Vector3.Distance(transform.position, target.position) < 0.01f)
        {
            movingUp = !movingUp;  // Reverse direction
        }
    }
}

*/
