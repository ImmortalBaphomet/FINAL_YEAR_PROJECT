using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Pause_Menu_Script : MonoBehaviour
{
    
    public GameObject pauseMenuUI;  
    public GameObject ControlMenuUI; 
    public bool isPaused = false;




    private void Start()
    {
        
        ControlMenuUI.SetActive(false);
        
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) 
        {
            if (isPaused)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
            }
        }


    }
    //////////////////////////////////////////////////////////////////////////////////////////
    public void NextChapter()
    {
        SceneManager.LoadScene(3);
    }
    //////////////////////////////////////////////////////////////////////////////////////////
    public void Play_Again()
    {
     
        // Reload the current scene
        SceneManager.LoadScene(1);
        
        // Optionally log the reload for debugging
        Debug.Log("Scene Reloaded");
    }
    ///////////////////////////////////////////////////////////////////////////////////////////
    public void ResumeGame()
    {
        pauseMenuUI.SetActive(false); 
        Time.timeScale = 1f; 
        isPaused = false;
    }
    public void PauseGame()
    {
        pauseMenuUI.SetActive(true); 
        Time.timeScale = 0f; 
        isPaused = true;
    }
    ///////////////////////////////////////////////////////////////////////////////////////////
    public void Controls()
    {
        ControlMenuUI.SetActive(true);
    }
    public void ControlBack()
    {
        ControlMenuUI.SetActive(false);
    }
    ///////////////////////////////////////////////////////////////////////////////////////////
    public void Back_Button()
    {
        SceneManager.LoadScene(0);
    }

}
