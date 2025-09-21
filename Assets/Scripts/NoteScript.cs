using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NoteScript : MonoBehaviour
{
    public GameObject noteUI;        
    public GameObject buttonPrompt;   
    public Sprite[] notes; 
    public Image note;
    public Button nextButton;
    public Button backButton;

    private int currentPage = 0;
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
                buttonPrompt.SetActive(false);
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
                currentPage = 0;
                ShowPage(currentPage);
            }
            else
            {
                Debug.LogWarning("Note UI not assigned.");
            }
        }

        if (playerInRange && Input.GetKeyDown(KeyCode.Space))
        {
            if (noteUI != null)
            {
                noteUI.SetActive(false);
                buttonPrompt.SetActive(true);
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
            }
            else
            {
                Debug.LogWarning("Note UI not assigned.");
            }
        }
    }

    public void PageFlipForward()
    {
        if (currentPage < notes.Length - 1)
        {
            currentPage++;
            ShowPage(currentPage);
        }
    }


    public void PageFlipBackward()
    {
        if (currentPage > 0)
        {
            currentPage--;
            ShowPage(currentPage);
        }
    }

 private void ShowPage(int pageIndex)
    {
        note.sprite = notes[pageIndex];
        backButton.interactable = (pageIndex > 0);
        nextButton.interactable = (pageIndex < notes.Length - 1);
    }

}