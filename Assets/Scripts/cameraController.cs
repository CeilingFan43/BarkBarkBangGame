using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraController : MonoBehaviour
{
    public Transform playerOrientation;
    public Transform player;
    public Transform playerObject;
    public Rigidbody rb;
    public Transform lookPoint;
    
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
        Vector3 viewDirection = lookPoint.position - new Vector3(transform.position.x, lookPoint.position.y, transform.position.z);

        // Setting the player orientation direction
        playerOrientation.forward = viewDirection.normalized;

        //setting player rotation
        playerObject.forward = viewDirection.normalized;
        
        
    }
}
