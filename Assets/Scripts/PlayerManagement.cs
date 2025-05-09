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
    private gamemanager gameManager;

    //Flashlight
    public Light flashlight;

    public GameObject player;
    public Mesh Stage1Mesh;
  

    public Mesh Stage2Mesh;
    public Material Stage2Mat;


    private MeshFilter playerMesh;
    private MeshRenderer playerRend;

    public Image health;
    public Image battery;


    // Start is called before the first frame update
    void Start()
    {
        gameManager = FindObjectOfType<gamemanager>();
        currentHealth = maxHealth;
        currentBattery = maxBattery;   
        player = GameObject.Find("PlayerObject");
        playerMesh = player.GetComponent<MeshFilter>();
        playerRend = player.GetComponent<MeshRenderer>();


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

    // Update is called once per frame
    void LateUpdate()
    {
        if(currentHealth <= 50 && currentHealth > 0)
        {
            Material[] materials = playerRend.materials;
            Debug.Log(materials);

            playerMesh.mesh = Stage2Mesh;
            Debug.Log("Half health");

        }

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

    public void Stage1Change(MeshFilter myMesh, MeshRenderer myRend)
    {
        
    }
}
