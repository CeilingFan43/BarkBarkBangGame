using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyScript : MonoBehaviour
{
    public float moveSpeed;
    private Transform playerPos;
    private CharacterController characterController;
    private Vector3 moveDirection;

    public float health;

    void Start()
    {
        characterController = GetComponent<CharacterController>();
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            playerPos = player.transform;
        }
    }


    // Update is called once per frame
    void Update()
    {
        moveDirection = ( playerPos.position - transform.position).normalized;
        moveDirection.y = 0;
        characterController.Move(moveDirection*moveSpeed*Time.deltaTime);
    }


    public void TakeDamage(float amount)
    {
        health -= amount;
        Debug.Log($"{gameObject.name} took {amount} damage, {health} HP left.");

        if (health <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Debug.Log($"{gameObject.name} died.");
        Destroy(gameObject);
    }
}
