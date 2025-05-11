using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class batteryPickup : MonoBehaviour
{
    public float batteryAmount = 20f;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerManagement player = other.GetComponent<PlayerManagement>();
            if (player != null)
            {
                player.BatteryPickup(batteryAmount);
                Destroy(gameObject); // Destroy pickup after use
            }
        }
    }
}