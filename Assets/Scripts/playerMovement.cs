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

    public float gravity = -5f;
    private Vector3 gravityVelocity;

    private CharacterController player;
    public Transform playerOrientation;
    private Vector3 moveDirection;

    void Start()
    {
        player = GetComponent<CharacterController>();
        currentStamina = maxStamina;
    }

   void Update()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        if (Input.GetKeyDown("left shift"))
        {
            running = true;
        }
        else if (Input.GetKeyUp("left shift"))
        {
            running = false;
        }

        Vector3 forward = playerOrientation.forward;
        Vector3 right = playerOrientation.right;

        forward.y = 0;
        right.y = 0;
        forward.Normalize();
        right.Normalize();

        moveDirection = (forward * vertical + right * horizontal).normalized;


        if (!player.isGrounded)
        {
            gravityVelocity.y += gravity * Time.deltaTime;
            player.Move(gravityVelocity.y * Time.deltaTime * Vector3.up + moveDirection * (running ? runSpeed : moveSpeed) * Time.deltaTime);
        }
        else
        {
            gravityVelocity.y = 0f; // Reset vertical velocity when grounded
            player.Move(moveDirection * (running ? runSpeed : moveSpeed) * Time.deltaTime);
        }

        //drain stamina.  Removed the check for isGrounded
        if (running)
        {
            currentStamina -= runCost * Time.deltaTime;
            if (currentStamina < 0)
            {
                currentStamina = 0;
                running = false;
            }
            stamina.fillAmount = currentStamina / maxStamina;
        }

        else
        {
            currentStamina += staminaRegen * Time.deltaTime;
            if (currentStamina > 100)
            {
                currentStamina = 100;
            }
            stamina.fillAmount = currentStamina / maxStamina;
        }
    }

    void FixedUpdate()
    {

    }
}