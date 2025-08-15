using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class cameraController : MonoBehaviour
{
    public Transform rotatePoint;       
    public Transform cameraPivot;
    public Camera mainCam;
    public float sensitivityX = 3f;
    public float sensitivityY = 3f;
    private float pitch = 0f; 

    void Start()
    {
        //Initialising pitch 
        Vector3 angles = transform.position - cameraPivot.position;
        pitch = 0f;

        //Locking cursor
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        //User mouse input
        float mouseX = Input.GetAxis("Mouse X") * sensitivityX;
        float mouseY = Input.GetAxis("Mouse Y") * sensitivityY;

        //Rotating camera
        cameraPivot.RotateAround(rotatePoint.position, Vector3.up, mouseX);
        cameraPivot.RotateAround(rotatePoint.position, cameraPivot.right, -mouseY);
    }
        
}
