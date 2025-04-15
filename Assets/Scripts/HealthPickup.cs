using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPickup : MonoBehaviour
{
    public float healthAmount = 20f;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerManagement player = other.GetComponent<PlayerManagement>();
            if (player != null)
            {
                player.HealthPickup(healthAmount);
                Destroy(gameObject); // Destroy pickup after use
            }
        }
    }
}