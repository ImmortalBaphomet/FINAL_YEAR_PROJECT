using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallSpear : TrapMaster
{
    public bool wallSpearActivate = false;
    [SerializeField] private List<GameObject> spears; 
    [SerializeField] private float spearDelay = 0.5f; // time delay between each spear thrust
    [SerializeField] private float thrustDistance = 2f; // distance of each spear thrusts out
    [SerializeField] private float thrustSpeed = 2f;//spear thrust speed

    private List<Vector3> spearRestPositions = new List<Vector3>(); // rest position of spears.

    void Start()
    {
        
        
    }

    // Update is called once per frame
    void Update()
    {
        foreach (GameObject spear in spears)
        {
            spearRestPositions.Add(spear.transform.position);
        }

       if (wallSpearActivate)
        {
             
            StartCoroutine(SpearWallTrap());
        }
    }

    public override void Activate()
    {
        wallSpearActivate = true;
    }
    public override void Deactivate()// not implemented the logic for this yet
    {
        wallSpearActivate = false;
    }

    IEnumerator SpearWallTrap()
    {
        for (int i = 0; i < spears.Count; i++)
        {
            StartCoroutine(ThrustSpear(spears[i], spearRestPositions[i]));
            yield return new WaitForSeconds(spearDelay);
        }
    }

    IEnumerator ThrustSpear(GameObject spear, Vector3 restPosition)
    {
        Vector3 thrustPosition = restPosition + spear.transform.up * thrustDistance;// from rest to the thrustdistance point b.

        
        while (Vector3.Distance(spear.transform.position, thrustPosition) > 0.01f)
        {
            spear.transform.position = Vector3.MoveTowards(spear.transform.position, thrustPosition, thrustSpeed * Time.deltaTime);
            yield return null;
        }

        yield return new WaitForSeconds(0.5f); 

        
        while (Vector3.Distance(spear.transform.position, restPosition) > 0.01f)
        {
            spear.transform.position = Vector3.MoveTowards(spear.transform.position, restPosition, thrustSpeed * Time.deltaTime);
            yield return null;
        }
    }

    
}
/*
*/
