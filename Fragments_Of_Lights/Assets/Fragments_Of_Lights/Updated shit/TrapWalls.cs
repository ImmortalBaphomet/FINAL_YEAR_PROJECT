using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapWalls : TrapMaster
{
    [SerializeField] private float wallSpeed;
    [SerializeField] private Transform targetWall;
    public bool wallActivate = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(wallActivate)
        {
            ActivateWallTrap();
        }
    }
    public override void Activate()
    {
        wallActivate = true;
    }


    public void ActivateWallTrap()
    {
        transform.position = Vector3.MoveTowards(transform.position,targetWall.position, wallSpeed * Time.deltaTime);
    }
}
