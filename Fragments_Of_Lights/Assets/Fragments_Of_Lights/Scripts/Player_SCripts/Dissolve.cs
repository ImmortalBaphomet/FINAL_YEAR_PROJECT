using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dissolve : MonoBehaviour
{
    public Material targetMaterial; // Assign the material using the shader
    public string propertyName = "_SetOn"; // Must match the float property name in Shader Graph
    public float speed = 0.5f; // Speed of change

    private float currentValue = 0f;
    // Start is called before the first frame update
    void Start()
    {
        // Apply to Shader
            targetMaterial.SetFloat(propertyName, currentValue);   
    }

    // Update is called once per frame
    void Update()
    {
       //value = Mathf.Lerp(value, 1f, speed * Time.deltaTime);
        if(Input.GetKeyDown(KeyCode.L))
        {
            currentValue = Mathf.Lerp(currentValue, 1f, speed * Time.deltaTime);
            Debug.Log("Dissolving NOw");
            
        }
       
    }





    
}
