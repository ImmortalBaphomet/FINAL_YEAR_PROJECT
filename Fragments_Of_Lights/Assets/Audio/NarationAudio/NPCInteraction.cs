using UnityEngine;
using TMPro;
using System.Collections.Generic; // Required for List

public class NPCInteraction : MonoBehaviour
{
    public GameObject npcPanel; // Panel containing the NPC dialogue UI
    public GameObject interactionPrompt; // Interaction prompt (e.g., "Press F to interact")
    public TextMeshProUGUI dialogueText;
    public List<string> dialogues; // Using List instead of array
    private int currentIndex = 0;
    private bool isPlayerInRange = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            interactionPrompt.SetActive(true); // Show the interaction prompt
            isPlayerInRange = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            interactionPrompt.SetActive(false); // Hide the interaction prompt
            EndNPCDialogue();
            isPlayerInRange = false;
        }
    }

    private void Update()
    {
        if (isPlayerInRange && Input.GetKeyDown(KeyCode.F))
        {
            StartNPCDialogue();
        }
    }

    public void StartNPCDialogue()
    {
        if (dialogues == null || dialogues.Count == 0 || dialogueText == null || npcPanel == null)
        {
            Debug.LogError("Dialogue or UI references are missing.");
            return;
        }

        currentIndex = 0;
        npcPanel.SetActive(true);
        DisplayNextLine();
    }

    public void DisplayNextLine()
    {
        if (currentIndex < dialogues.Count) // Updated to use List.Count
        {
            dialogueText.text = dialogues[currentIndex];
            currentIndex++;
        }
        else
        {
            EndNPCDialogue();
        }
    }

    public void EndNPCDialogue()
    {
        npcPanel.SetActive(false);
    }

    // Call this function when the Exit button is clicked
    public void OnExitButtonClicked()
    {
        EndNPCDialogue();
    }
}
