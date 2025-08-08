using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraController : MonoBehaviour
{
    public Transform player;
    public float rotationSpeed;


    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        Vector3 cameraForward = Camera.main.transform.forward;

        Vector3 LookDir = new Vector3(cameraForward.x, 0f, cameraForward.z).normalized;
        player.forward = LookDir;
    }
}
