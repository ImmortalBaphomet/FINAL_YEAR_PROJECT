using UnityEngine;
using TMPro;
using System.Collections.Generic; // Required for List

public class NarratorTrigger : MonoBehaviour
{
    public GameObject narratorPanel; // Panel containing the Narrator dialogue UI
    public TextMeshProUGUI dialogueText;
    public List<string> dialogues; // Using List instead of array
    private int currentIndex = 0;
    private bool isActive = false; // Check if narrator dialogue is active

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            StartNarratorDialogue();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            EndNarratorDialogue();
        }
    }

    public void StartNarratorDialogue()
    {
        if (dialogues == null || dialogues.Count == 0 || dialogueText == null || narratorPanel == null)
        {
            Debug.LogError("Dialogue or UI references are missing.");
            return;
        }

        currentIndex = 0;
        isActive = true;
        narratorPanel.SetActive(true);
        DisplayNextLine();
    }

    public void DisplayNextLine()
    {
        Debug.Log("DisplayNextLine button clicked"); // Add this line

        if (!isActive) return;

        if (currentIndex < dialogues.Count)
        {
            dialogueText.text = dialogues[currentIndex];
            currentIndex++;
        }
        else
        {
            EndNarratorDialogue();
        }
    }


    public void EndNarratorDialogue()
    {
        isActive = false;
        narratorPanel.SetActive(false);
    }

    // Call this function when the Exit button is clicked
    public void OnExitButtonClicked()
    {
        EndNarratorDialogue();
    }
}
