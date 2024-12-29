using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Title_Screen : MonoBehaviour
{
    public float delay = 3f; 

    private bool isTransitioning = false;

    void Update()
    {
        if (Input.anyKeyDown && !isTransitioning)
        {
            StartCoroutine(ChangeSceneWithDelay());
        }
    }

    private IEnumerator ChangeSceneWithDelay()
    {
        isTransitioning = true; 
        yield return new WaitForSeconds(delay); 
        SceneManager.LoadScene(1); 
    }

}
