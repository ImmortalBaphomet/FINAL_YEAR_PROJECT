using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishCondition : MonoBehaviour
{
    [SerializeField] private LazerBeam lazerBeam;
    [SerializeField] private MeshRenderer endMesh;
    [SerializeField] private Color endColor;
    [SerializeField] private Color defColor;
    // Start is called before the first frame update
    void Start()
    {
        lazerBeam = FindAnyObjectByType<LazerBeam>();
        endMesh = GetComponent<MeshRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        LazerFinish();
    }

    void LazerFinish()
    {
        if(lazerBeam.hitFinish)
        {
            endMesh.material.color = endColor;
        }
        else
        {
            endMesh.material.color = defColor;
        }
    }
}
