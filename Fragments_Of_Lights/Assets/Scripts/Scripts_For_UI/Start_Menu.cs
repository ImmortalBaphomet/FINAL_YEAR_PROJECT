
using UnityEngine;
using UnityEngine.SceneManagement;

public class Start_Menu : MonoBehaviour
{
    public GameObject ControlPanel;
    public GameObject CreditsPanel;

    private void Start()
    {
        ControlPanel.SetActive(false);
        CreditsPanel.SetActive(false);
    }

    public void PlayGame()
    {
        SceneManager.LoadScene(2);
    }


    public void Controls()
    {
        ControlPanel.SetActive(true);
        Debug.Log("Control_OPen");
    }
    public void Credits()
    {
        CreditsPanel.SetActive(true);
        Debug.Log("Credit_Open");
    }
    public void BackButton()
    {
        ControlPanel.SetActive(false);
        CreditsPanel.SetActive(false);
    }



    public void LeaveGame()
    {
        Application.Quit();
        Debug.Log("Game_Leave");
    }




}
