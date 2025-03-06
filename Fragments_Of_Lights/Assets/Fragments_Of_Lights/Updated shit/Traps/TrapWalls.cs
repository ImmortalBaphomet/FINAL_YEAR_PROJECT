using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapWalls : TrapMaster
{
    [SerializeField] private float wallSpeed;
    [SerializeField] private Transform targetWall;
    public bool wallActivate = false;
    public bool reachPos = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(wallActivate && !reachPos)
        {
            ActivateWallTrap();
        }
        else
        {
            DeactivateWallMove();
        }
            
        
    }
    public override void Activate()
    {
        wallActivate = true;
    }
    public override void Deactivate()
    {
        //wallActivate = false;
    }


    public void ActivateWallTrap()
    {
        // Check if the wall has already reached the target
        if (Vector3.Distance(transform.position, targetWall.position) <= Mathf.Epsilon)
        {
            wallActivate = false;  // Stop movement
            Debug.Log("Wall reached the target and stopped.");
            DeactivateWallMove();
            return;
        }

        // Move towards the target position
        transform.position = Vector3.MoveTowards(transform.position, targetWall.position, wallSpeed * Time.deltaTime);
        Debug.Log("Wall Moving: " + transform.position);
    }

    private void DeactivateWallMove()
    {
        reachPos = true;
        wallActivate = false;
        wallSpeed = 0f;  // Stop the wall from moving
        Debug.Log("Wall reached the target. Movement stopped.");
    }
}
