using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//using UnityEngine.UIElements;

public class PlayerManagement : MonoBehaviour
{
    public float maxHealth = 100;
    private float currentHealth;
    public float maxBattery = 100;
    private float currentBattery;
    public float batteryDrain;
    private gameManager gameManager;

    //Flashlight
    public Light flashlight;

    public GameObject playerFull;
    public GameObject playerHalf;
    public GameObject playerDead;

    public Image health;
    public Image battery;

    public GameObject player;


    // Start is called before the first frame update
    void Start()
    {
        gameManager = FindObjectOfType<gameManager>();
        currentHealth = maxHealth;
        currentBattery = maxBattery;   
        player = GameObject.Find("Player");
        CheckHealth();



    }

    void Update()
    {
        if(Input.GetKeyDown("f"))
        {
            flashlight.enabled = !flashlight.enabled;
            //flashOn = !flashOn;
        }

        if (flashlight.enabled==true)
        {
            currentBattery -= batteryDrain * Time.deltaTime;
        }

        if (flashlight.enabled == true && currentBattery <= 0)
        {
            flashlight.enabled = false;
        }

        battery.fillAmount = currentBattery / maxBattery;
        health.fillAmount = currentHealth / maxHealth;


    }


    public void PlayerTakeDamage(float amount)
    {
        currentHealth -= amount;
        Debug.Log("taken damage instance");
        Debug.Log(currentHealth);
        CheckHealth();
    }

    public void HealthPickup(float amount)
    {
        currentHealth += amount;
        Debug.Log(currentHealth);
        CheckHealth();
    }

    public void BatteryPickup(float amount)
    {
        currentBattery += amount;
        Debug.Log(currentBattery);
    }

    public void CheckHealth()
    {
        if(currentHealth <= 0)
        {
            gameManager.PlayerDied();
        }

        else if(0 < currentHealth && currentHealth < 50)
        {
            playerFull.SetActive(false);
            playerHalf.SetActive(true);
            
        }
        else
        {
            playerFull.SetActive(true);
            playerHalf.SetActive(false);
        }
    }

}

