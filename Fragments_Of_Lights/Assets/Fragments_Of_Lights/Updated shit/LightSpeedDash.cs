using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightSpeedDash : MonoBehaviour
{
    [Header("Dash Settings")]
    [SerializeField] private float dashDistance = 10f; 
    [SerializeField] private float dashCooldown = 2f; 
    [SerializeField] private float dashDuration = 0.2f; 
    [SerializeField] private KeyCode dashKey;


    [Header("Trail Effect")]
    [SerializeField] private TrailRenderer dashTrail; 
    [SerializeField] private Renderer playerRenderer; 

    public bool canDash = true;
    private Vector3 dashDirection;

    // Start is called before the first frame update
    void Start()
    {
        canDash = true;
    }

    // Update is called once per frame
    void Update()
    {
         if (Input.GetKeyDown(dashKey) && canDash)
        {
            Dash();
        }
    }

    private void Dash()
    {
        if (!canDash) return;

        if(Input.GetKeyDown(KeyCode.Space))
        {
            dashDirection = (transform.forward + Vector3.up) * dashDistance; 
        }
        else{
            dashDirection = transform.forward * dashDistance; 
        }
        
        Vector3 dashTarget = transform.position + dashDirection;

        
        StartCoroutine(PerformDash(dashTarget));
    }

     private IEnumerator PerformDash(Vector3 targetPosition)
    {
        
        canDash = false;
        playerRenderer.enabled = false;
        if (dashTrail != null)
        {   
            dashTrail.enabled = true;
        } 

        
        float elapsedTime = 0f;
        Vector3 startPosition = transform.position;

        while (elapsedTime < dashDuration)
        {
            transform.position = Vector3.Lerp(startPosition, targetPosition, elapsedTime / dashDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

     
        transform.position = targetPosition;

        
        playerRenderer.enabled = true;
        if (dashTrail != null)
        {
            dashTrail.enabled = false;
            dashTrail.Clear();
        } 

        // Start cooldown timer
        yield return new WaitForSeconds(dashCooldown);
        canDash = true;
    }


}

/*
*/
