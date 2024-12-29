using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spike : MonoBehaviour
{
    Rigidbody spikeRB;
    [SerializeField] float dropForce = 5f;
    [SerializeField] Transform reSpawnPt;
    // Start is called before the first frame update
    void Start()
    {
        spikeRB.AddForce(Vector3.down * dropForce, ForceMode.Impulse);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnCollisionEnter(Collision other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            Destroy(other.gameObject);
            //other.gameObject.transform.position = reSpawnPt.position;
        }

        if(other.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            Destroy(this.gameObject);
        }
    }
}
