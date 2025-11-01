using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//using UnityEngine.UIElements;

public class PlayerManagement : MonoBehaviour
{
    public float maxHealth = 100;
    private float currentHealth;
    public float maxBattery = 1000;
    private float currentBattery;
    public float batteryDrain;
    private GameManager gameManager;

    //Animations
    public Animator playerAnimator;

    //Flashlight
    public Light flashlight;
    public Light spotlight;
    public Light faceLight;

    public GameObject playerHalf;
    public GameObject playerLow;

    public Image health;
    public Image battery;

    public GameObject player;


    //UI Health Layers
    public GameObject fullHealthUI;
    public GameObject halfHealthUI;
    public GameObject lowHealthUI;


    // Start is called before the first frame update
    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        currentHealth = maxHealth;
        currentBattery = maxBattery;   
        player = GameObject.Find("Player");
        CheckHealth();
        fullHealthUI.SetActive(true);



    }

    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Escape))
        {
        gameManager.Pause();
        }



        if(Input.GetKeyDown("f"))
        {
            flashlight.enabled = !flashlight.enabled;
            spotlight.enabled = !spotlight.enabled;
            faceLight.enabled = !faceLight.enabled;
            //flashOn = !flashOn;
        }

        if (flashlight.enabled==true)
        {
            currentBattery -= batteryDrain * Time.deltaTime;
        }

        if (flashlight.enabled == true && currentBattery <= 0)
        {
            flashlight.enabled = false;
            spotlight.enabled = false;
            faceLight.enabled = false;
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

    public void BatteryRefill()
    {
        currentBattery = maxBattery;
        Debug.Log("Battery Refilled");
    }

    public void CheckHealth()
    {
        if(0 < currentHealth && currentHealth < 25)
        {
            playerLow.SetActive(true);
            lowHealthUI.SetActive(true);
            halfHealthUI.SetActive(false);
            
        }

        else if(0 < currentHealth && currentHealth < 50)
        {
            playerHalf.SetActive(true);
            fullHealthUI.SetActive(false);
            halfHealthUI.SetActive(true);
            
        }

        else if(currentHealth <= 0)
        {
            gameManager.PlayerDied();
            
        }

        if(currentHealth >= 50)
        {
            playerHalf.SetActive(false);
            playerLow.SetActive(false);
            fullHealthUI.SetActive(true);
        }
    }

}

