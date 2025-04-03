using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropSpikes : MonoBehaviour
{
    [SerializeField] private Transform[] spikeDropPts;
    [SerializeField] private GameObject spikePreFab;
    // Start is called before the first frame update
    void Start()
    {

    }

    void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.gameObject.tag + "Entered the spike drop area");

        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("start the invoke");
            InvokeRepeating("SpikeSpawner", 0.5f, 1f);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("stop the invoke");
            CancelInvoke("SpikeSpawner");
        }
    }


    void SpikeSpawner()
    {
        int index = Random.Range(0, spikeDropPts.Length);
        Vector3 dropPos = new Vector3(spikeDropPts[index].position.x, spikeDropPts[index].position.y, spikeDropPts[index].position.z);

        Instantiate(spikePreFab, dropPos, spikePreFab.transform.rotation);
    }
}




