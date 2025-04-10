using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class playerMovement : MonoBehaviour
{
    public float moveSpeed = 5;
    public float runSpeed = 10;
    private bool running;
    public float runCost = 20;

    public float maxStamina = 100;
    public float currentStamina;
    public float staminaRegen = 10;
    public Image stamina;

    private CharacterController characterController;
    public Rigidbody player;
    public Transform playerOrientation;
    private Vector3 moveDirection;

    void Start()
    {
        characterController = GetComponent<CharacterController>();
        currentStamina = maxStamina;
    }


    // CHANGE OUT MOVEMENT FOR CHARACTER CONTROLLER NOT RIGIDBODY
    // CHANGE OUT MOVEMENT FOR CHARACTER CONTROLLER NOT RIGIDBODY
    // CHANGE OUT MOVEMENT FOR CHARACTER CONTROLLER NOT RIGIDBODY
    
    void Update()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        if(Input.GetKeyDown("left shift"))
        {
            running = true;
        }
        else if(Input.GetKeyUp("left shift"))
        {
        running = false;
        }

        Vector3 forward = playerOrientation.forward;
        Vector3 right = playerOrientation.right;

        forward.y = 0;
        right.y = 0; 
        forward.Normalize();
        right.Normalize();

        moveDirection = forward * vertical + right * horizontal;




        if(running == false)
        {
        /*
        player.MovePosition(player.position + moveDirection * moveSpeed * Time.fixedDeltaTime);
        */
        characterController.Move(moveDirection*moveSpeed*Time.deltaTime);
        currentStamina += staminaRegen * Time.deltaTime;
            if(currentStamina > 100)
            {
                currentStamina = 100;
            }
        stamina.fillAmount = currentStamina / maxStamina;

        }

        
        else
        {
            /*
        player.MovePosition(player.position + moveDirection * runSpeed * Time.fixedDeltaTime);
        */
        characterController.Move(moveDirection*runSpeed*Time.deltaTime);
        currentStamina -= runCost * Time.deltaTime;
            if(currentStamina < 0)
            {
                currentStamina = 0;
            }
        stamina.fillAmount = currentStamina / maxStamina;
        }
        
    }

    void FixedUpdate()
    {

    }
}