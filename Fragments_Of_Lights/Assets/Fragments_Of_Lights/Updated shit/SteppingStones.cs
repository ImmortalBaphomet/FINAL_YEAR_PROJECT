using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SteppingStones : MonoBehaviour
{
    [SerializeField] private bool stoneReal = true; // tick to true if the stone is real or false if stone is fake
    private Collider stoneCollider;
    private Renderer stoneRenderer;
    private bool playerTrapped = false;

    // Start is called before the first frame update
    void Start()
    {
        stoneCollider = GetComponent<BoxCollider>();
        stoneRenderer = GetComponent<Renderer>();

        UpdateStoneState();
    }

    // When the player steps on the stone
    private void OnTriggerEnter(Collider other)
    {
        if (!stoneReal && other.CompareTag("Player")) 
        {
            playerTrapped = true;
            StartCoroutine(FakeStoneBehavior());

        }
        
    }
    private void OnTriggerExit(Collider other)
    {
        playerTrapped = false;
    }

    
    private IEnumerator FakeStoneBehavior()
    {
        
        stoneRenderer.material.color = Color.red; 

        
        yield return new WaitForSeconds(0.5f);

        stoneCollider.isTrigger = true; 
    }

    // Update the stone's initial state
    private void UpdateStoneState()
    {
        if (!stoneReal)
        {
            stoneCollider.isTrigger = true; 
            stoneRenderer.material.color = Color.gray; 

            // changes the layer to default from ground, preventing player from jumping.
            gameObject.layer = LayerMask.NameToLayer("Default"); 
        }
        else
        {
            stoneCollider.isTrigger = false; 
        }
    }
}
