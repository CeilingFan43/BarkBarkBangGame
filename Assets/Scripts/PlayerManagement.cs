using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerManagement : MonoBehaviour
{
    public float maxHealth = 100;
    private float currentHealth;
    public float maxBattery = 100;
    private float currentBattery;
    public float batteryDrain;
    private gamemanager gameManager;
    //public float batteryRegen;

    //Flashlight
    public Light flashlight;
    //private bool flashOn = true;




    public Image health;
    public Image battery;


    // Start is called before the first frame update
    void Start()
    {
        gameManager = FindObjectOfType<gamemanager>();
        currentHealth = maxHealth;
        currentBattery = maxBattery;        
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

        /*
        if((flashlight.enabled == true) && (currentBattery) > 0)
        {
            currentBattery -= batteryDrain * Time.deltaTime;
        }

        else if (currentBattery <= 0)
        {
            flashlight.enabled = false;
        }

        else if(flashlight.enabled == false)
        {
            currentBattery += batteryRegen * Time.deltaTime;
        }


        if(currentBattery > maxBattery)
        {
            currentBattery = maxBattery;
        }
        */
        battery.fillAmount = currentBattery / maxBattery;
        health.fillAmount = currentHealth / maxHealth;


    }

    // Update is called once per frame
    void LateUpdate()
    {
        if(currentHealth <= 0)
        {
            gameManager.PlayerDied();
        }
    }

    public void PlayerTakeDamage(float amount)
    {
        currentHealth -= amount;
        Debug.Log("taken damage instance");
        Debug.Log(currentHealth);
    }

    public void HealthPickup(float amount)
    {
        currentHealth += amount;
        Debug.Log(currentHealth);
    }

    public void batteryPickup(float amount)
    {
        currentBattery += amount;
        Debug.Log(currentBattery);
    }
}
