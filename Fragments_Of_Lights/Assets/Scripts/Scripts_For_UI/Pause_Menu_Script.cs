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
        Time.timeScale = 1f;  // Ensure the game starts unpaused in all scenes
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
        //if(SceneManager.GetActiveScene().buildIndex == ) 
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex+1);
    }
    //////////////////////////////////////////////////////////////////////////////////////////
    public void Play_Again()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); // Reload the current scene
        Time.timeScale = 1f; // Ensure time scale is reset
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
        Time.timeScale = 0f;// 
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
        Time.timeScale = 1f;
    }

}
