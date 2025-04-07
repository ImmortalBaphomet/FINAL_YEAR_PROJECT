using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Roller : MonoBehaviour
{
    public float moveSpeed = 3f; // Speed of vertical movement
    public float moveHeight = 2f; // Maximum height difference
    public float rotationSpeed = 100f; // Speed of rotation
    public Vector3 rotationAxis = Vector3.right; // Rotation axis (default is X-axis)

    private Vector3 startPosition;
    private bool movingUp = true;

    void Start()
    {
        startPosition = transform.position;
    }

    void Update()
    {
        // Move up and down
        float newY = transform.position.y + (movingUp ? moveSpeed : -moveSpeed) * Time.deltaTime;
        if (newY >= startPosition.y + moveHeight)
        {
            newY = startPosition.y + moveHeight;
            movingUp = false;
        }
        else if (newY <= startPosition.y - moveHeight)
        {
            newY = startPosition.y - moveHeight;
            movingUp = true;
        }
        transform.position = new Vector3(transform.position.x, newY, transform.position.z);

        // Rotate around the specified axis
        transform.Rotate(rotationAxis * rotationSpeed * Time.deltaTime);
    }
}



