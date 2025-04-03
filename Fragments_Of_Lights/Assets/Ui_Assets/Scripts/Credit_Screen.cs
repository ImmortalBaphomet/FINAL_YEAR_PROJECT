using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;

public class Credit_Screen : MonoBehaviour
{
    public Light spotlight;
    public float targetIntensity = 1000f;

    public float speed = 5f;
    public Player_Scene ps;

    private void Update()
    {
        if (ps.Glass_Area)
        {

            spotlight.intensity = Mathf.Lerp(spotlight.intensity, targetIntensity, speed * Time.deltaTime);

        }
       
    }


    

}
