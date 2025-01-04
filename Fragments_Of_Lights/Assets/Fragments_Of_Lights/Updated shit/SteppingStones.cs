using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SteppingStones : MonoBehaviour
{
    [SerializeField] private bool stoneReal = true; // tick to true if the stone is real or false if stone is fake
    private Collider stoneCollider;
    private Renderer stoneRenderer;

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
            
            StartCoroutine(FakeStoneBehavior());
        }
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
        }
        else
        {
            stoneCollider.isTrigger = false; 
        }
    }
}
