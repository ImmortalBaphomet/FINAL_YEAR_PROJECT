using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MirrorSelect : MonoBehaviour
{
    public List<MirrorControl> mirrors; // All mirrors in the maze
    private int selectedMirrorIndex = 0;   // Index of the currently selected mirror

    // Start is called before the first frame update
    void Start()
    {
        SelectMirror(0); // Initialize by selecting the first mirror
    }

    // Update is called once per frame
    void Update()
    {
        // Change selection based on arrow keys
        if (Input.GetKeyDown(KeyCode.UpArrow)) MoveSelection(Vector3.forward);
        if (Input.GetKeyDown(KeyCode.DownArrow)) MoveSelection(Vector3.back);
        if (Input.GetKeyDown(KeyCode.LeftArrow)) MoveSelection(Vector3.left);
        if (Input.GetKeyDown(KeyCode.RightArrow)) MoveSelection(Vector3.right);

        // Select the next mirror in the list when "Enter" is pressed
        if (Input.GetKeyDown(KeyCode.Return)) SelectNextMirror();
    }

    void MoveSelection(Vector3 direction)
    {
        // Find the nearest mirror in the given direction
        MirrorControl currentMirror = mirrors[selectedMirrorIndex];
        float minDistance = Mathf.Infinity;
        int newSelectionIndex = selectedMirrorIndex;

        for (int i = 0; i < mirrors.Count; i++)
        {
            if (i == selectedMirrorIndex) continue; // Skip the currently selected mirror

            Vector3 toMirror = mirrors[i].transform.position - currentMirror.transform.position;
            if (Vector3.Dot(toMirror.normalized, direction.normalized) > 0.8f) // Check alignment with direction
            {
                float distance = toMirror.magnitude;
                if (distance < minDistance)
                {
                    minDistance = distance;
                    newSelectionIndex = i;
                }
            }
        }

        if (newSelectionIndex != selectedMirrorIndex)
        {
            SelectMirror(newSelectionIndex);
        }
    }

    void SelectNextMirror()
    {
        // Deselect the current mirror
        mirrors[selectedMirrorIndex].SelectMirror(false);

        // Increment the index and wrap around if necessary
        selectedMirrorIndex = (selectedMirrorIndex + 1) % mirrors.Count;

        // Select the next mirror
        mirrors[selectedMirrorIndex].SelectMirror(true);
    }

    void SelectMirror(int index)
    {
        // Deselect current mirror
        if (selectedMirrorIndex >= 0 && selectedMirrorIndex < mirrors.Count)
        {
            mirrors[selectedMirrorIndex].SelectMirror(false);
        }

        // Select new mirror
        selectedMirrorIndex = index;
        if (selectedMirrorIndex >= 0 && selectedMirrorIndex < mirrors.Count)
        {
            mirrors[selectedMirrorIndex].SelectMirror(true);
        }
    }


}
/*
    public List<MirrorControl> mirrors; // All mirrors in the maze
    private int selectedMirrorIndex = 0;   // Index of the currently selected mirror

    // Start is called before the first frame update
    void Start()
    {
        SelectMirror(0); // Initialize by selecting the first mirror
    }

    // Update is called once per frame
    void Update()
    {
        // Change selection based on arrow keys
        if (Input.GetKeyDown(KeyCode.UpArrow)) MoveSelection(Vector3.forward);
        if (Input.GetKeyDown(KeyCode.DownArrow)) MoveSelection(Vector3.back);
        if (Input.GetKeyDown(KeyCode.LeftArrow)) MoveSelection(Vector3.left);
        if (Input.GetKeyDown(KeyCode.RightArrow)) MoveSelection(Vector3.right);

        // Select the next mirror in the list when "Enter" is pressed
        if (Input.GetKeyDown(KeyCode.Return)) SelectNextMirror();
    }

    void MoveSelection(Vector3 direction)
    {
        // Find the nearest mirror in the given direction
        MirrorControl currentMirror = mirrors[selectedMirrorIndex];
        float minDistance = Mathf.Infinity;
        int newSelectionIndex = selectedMirrorIndex;

        for (int i = 0; i < mirrors.Count; i++)
        {
            if (i == selectedMirrorIndex) continue; // Skip the currently selected mirror

            Vector3 toMirror = mirrors[i].transform.position - currentMirror.transform.position;
            if (Vector3.Dot(toMirror.normalized, direction.normalized) > 0.8f) // Check alignment with direction
            {
                float distance = toMirror.magnitude;
                if (distance < minDistance)
                {
                    minDistance = distance;
                    newSelectionIndex = i;
                }
            }
        }

        if (newSelectionIndex != selectedMirrorIndex)
        {
            SelectMirror(newSelectionIndex);
        }
    }

    void SelectNextMirror()
    {
        // Deselect the current mirror
        mirrors[selectedMirrorIndex].SelectMirror(false);

        // Increment the index and wrap around if necessary
        selectedMirrorIndex = (selectedMirrorIndex + 1) % mirrors.Count;

        // Select the next mirror
        mirrors[selectedMirrorIndex].SelectMirror(true);
    }

    void SelectMirror(int index)
    {
        // Deselect current mirror
        if (selectedMirrorIndex >= 0 && selectedMirrorIndex < mirrors.Count)
        {
            mirrors[selectedMirrorIndex].SelectMirror(false);
        }

        // Select new mirror
        selectedMirrorIndex = index;
        if (selectedMirrorIndex >= 0 && selectedMirrorIndex < mirrors.Count)
        {
            mirrors[selectedMirrorIndex].SelectMirror(true);
        }
    }
*/
