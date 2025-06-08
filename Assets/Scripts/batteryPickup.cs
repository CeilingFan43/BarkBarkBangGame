using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class batteryPickup : MonoBehaviour
{
    public float batteryAmount = 200f;
    public float frequency = 0.5f;
    public float amplitude = 1f;

    private Vector3 startPosition;
    private float time;

    //floating code
    void Start()
    {
        startPosition = transform.position;
        time = 0f;
    }

    void Update()
    {
        time += Time.deltaTime;
        float verticalOffset = amplitude * Mathf.Sin(2 * Mathf.PI * frequency * time);
        transform.position = startPosition + new Vector3(0f, verticalOffset, 0f);
    }


    //battery pickup code
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