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

    public float damage = 10f;
    public float attackCooldown = 2f;

    private bool canAttack = true;

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
        Quaternion toRotation = Quaternion.LookRotation(moveDirection, Vector3.up);
        transform.rotation = Quaternion.Slerp(transform.rotation, toRotation, 10f * Time.deltaTime);
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

     private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player") && canAttack)
        {
            PlayerManagement pm = other.GetComponent<PlayerManagement>();
            if (pm != null)
            {
                Debug.Log("enemy attacked");
                pm.PlayerTakeDamage(damage);
                StartCoroutine(StartAttackCooldown());
            }
        }
    }

    private System.Collections.IEnumerator StartAttackCooldown()
    {
        canAttack = false;
        yield return new WaitForSeconds(attackCooldown);
        canAttack = true;
    }

}
