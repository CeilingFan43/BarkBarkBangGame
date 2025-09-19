using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FuelRefill : MonoBehaviour
{
    public GameObject buttonPrompt;   

    private PlayerManagement player;

    private bool playerInRange = false;

    void Start()
    {
        player = FindObjectOfType<PlayerManagement>();
    }
    

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            //PlayerManagement player = other.GetComponent<PlayerManagement>();

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

    void Update()
    {
        if(Input.GetKeyDown("e") && playerInRange)
        {
            player.BatteryRefill();
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
        }
    }
}
