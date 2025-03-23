using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraController : MonoBehaviour
{
    public Transform playerOrientation;
    public Transform player;
    public Transform playerObject;
    public Rigidbody rb;
    
    public float rotationSpeed;



    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        // Getting the player direction vector
        Vector3 viewDirection = player.position - new Vector3(transform.position.x, player.position.y, transform.position.z);

        // Setting the player direction
        playerOrientation.forward = viewDirection.normalized;

        // Rotating the player
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        Vector3 inputDirection = playerOrientation.forward*verticalInput + playerOrientation.right*horizontalInput;

        if(inputDirection != Vector3.zero)
        {
            playerObject.forward = Vector3.Slerp(playerObject.forward, inputDirection.normalized, Time.deltaTime*rotationSpeed);
        }
        
    }
}
