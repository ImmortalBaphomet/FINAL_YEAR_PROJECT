using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapWalls : TrapMaster
{
    [SerializeField] private float wallSpeed;
    [SerializeField] private Transform targetWall;
    [SerializeField] private float stopThreshold = 0.5f;
    public bool wallActivate = false;
    public bool reachPos = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(transform.position.magnitude == targetWall.position.magnitude)
        {
            DeactivateWallMove();
            
        }
        else if(wallActivate && !reachPos)
        {
            ActivateWallTrap();
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
