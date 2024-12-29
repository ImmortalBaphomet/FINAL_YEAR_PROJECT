using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spikes : MonoBehaviour
{
    Rigidbody spikeRB;
    [SerializeField] float dropForce = 15f;
    //[SerializeField] Transform reSpawnPt;
    // Start is called before the first frame update
    void Start()
    {
        spikeRB = GetComponent<Rigidbody>();

        spikeRB.AddForce(Vector3.down * dropForce, ForceMode.Impulse);
    }

    // Update is called once per frame
    void Update()
    {

    }
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer== LayerMask.NameToLayer("Ground"))
        {
            Destroy(this.gameObject);
            //other.gameObject.transform.position = reSpawnPt.position;
        }
    }
    
}

