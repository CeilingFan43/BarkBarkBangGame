using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorLogic : MonoBehaviour
{
    public GameObject Door;
    public GameObject doorPrompt;


    public bool doorIsOpen = false;
    public float doorOpenAngle = 120f;
    public float doorClosedAngle = 0f;

    private bool playerInRange = false;

    public int health = 100;

    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (playerInRange && Input.GetKeyDown(KeyCode.E))
        {
            DoorInteract();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = true;

            if (doorPrompt != null)
            {
                doorPrompt.SetActive(true);
            }
            else
            {
                Debug.LogWarning("No Prompt set.");
            }
        }
    }

      private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;

            if (doorPrompt != null)
            {
                doorPrompt.SetActive(false);
                
            }
        }
    }

public void DoorInteract()
{
    if (!playerInRange) return;
    doorIsOpen = !doorIsOpen;
    StartCoroutine(RotateDoor());
}

public IEnumerator RotateDoor()
{
    float duration = 1f;
    float timeElapsed = 0f;

    float startAngle = Door.transform.localEulerAngles.y;
    float targetAngle = doorIsOpen ? doorOpenAngle : doorClosedAngle;

    while (timeElapsed < duration)
    {
        timeElapsed += Time.deltaTime;

        float currentAngle = Mathf.Lerp(startAngle, targetAngle, timeElapsed / duration);

        Door.transform.localEulerAngles = new Vector3(Door.transform.localEulerAngles.x, currentAngle, Door.transform.localEulerAngles.z
        );
        yield return null;
    }

    yield break;
}
}