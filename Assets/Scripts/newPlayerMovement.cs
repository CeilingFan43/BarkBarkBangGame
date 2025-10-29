using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class newPlayerMovement : MonoBehaviour
{
    //Setting up variables
    public float moveSpeed = 5;
    public float runSpeed = 10;
    private bool running;
    public float runCost = 20;

    public float maxStamina = 100;
    public float currentStamina;
    public float staminaRegen = 10;
    public float rotationSpeed = 5f;

    public Transform cameraPos;

    private Vector3 moveDirection;

    //Setting up dependencies
    public Image stamina;
    private CharacterController player;
    public Transform playerOrientation;
    public Animator playerAnimator;



    // Start is called before the first frame update
    void Start()
    {
        player = GetComponent<CharacterController>();
        currentStamina = maxStamina;   
    }

    // Update is called once per frame
    void Update()
    {
        //WASD Input values
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

        //Movement Vectors
        Vector3 right = cameraPos.right;
        Vector3 forward = Vector3.Cross(right, Vector3.up);



        //Moving the Player
        moveDirection = (forward * vertical + right * horizontal) * (running ? runSpeed : moveSpeed);
        player.SimpleMove(moveDirection);

        //Rotating the player to movementDirection
        if (moveDirection != Vector3.zero)
        {
        playerOrientation.rotation = Quaternion.Slerp (playerOrientation.rotation, Quaternion.LookRotation(moveDirection), Time.deltaTime * rotationSpeed);
        }

        //drain stamina.
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

        //Animations
        bool isMoving = player.velocity.magnitude > 0.1f;

        if (isMoving)
        {
            if (running)
            {
                playerAnimator.SetBool("PlayerRunning", true);
                playerAnimator.SetBool("PlayerWalking", false);
            }

            else
            {
                playerAnimator.SetBool("PlayerWalking", true);
                playerAnimator.SetBool("PlayerRunning", false);
            }
        }

        else
        {
            playerAnimator.SetBool("PlayerWalking", false);
            playerAnimator.SetBool("PlayerRunning", false);
        }

    }
}
