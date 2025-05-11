using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NoteScript : MonoBehaviour
{
    public GameObject noteUI;         // Assigned in Inspector
    public GameObject buttonPrompt;   // Assigned in Inspector

    private bool playerInRange = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = true;

            if (buttonPrompt != null)
            {
                buttonPrompt.SetActive(true);
            }
            else
            {
                Debug.LogWarning("No Prompt set.");
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;

            if (buttonPrompt != null)
            {
                buttonPrompt.SetActive(false);
            }

            if (noteUI != null)
            {
                noteUI.SetActive(false);
            }
        }
    }

    private void Update()
    {
        if (playerInRange && Input.GetKeyDown(KeyCode.E))
        {
            if (noteUI != null)
            {
                noteUI.SetActive(true);
            }
            else
            {
                Debug.LogWarning("Note UI not assigned.");
            }
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (noteUI != null)
            {
                noteUI.SetActive(false);
            }
            else
            {
                Debug.LogWarning("Note UI not assigned.");
            }
        }
    }
}