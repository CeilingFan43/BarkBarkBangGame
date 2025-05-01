using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    private NavMeshAgent agent;
    private Transform player;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        GameObject playerPos = GameObject.FindGameObjectWithTag("Player");
        if (playerPos != null)
        {
            player = playerPos.transform;
        }
        else
        {
            Debug.LogError("Player GameObject not found. Make sure the Player is tagged 'Player'.");
        }

        if (agent == null)
        {
            Debug.LogError("NavMeshAgent not assigned in the Inspector!  Make sure to drag the NavMeshAgent component from the Inspector to the 'Agent' field of the EnemyAI script.");
            return; // Important: Exit if no agent
        }

        // Raycast to find the nearest NavMesh point
        NavMeshHit hit;
        if (NavMesh.SamplePosition(transform.position, out hit, 1.0f, NavMesh.AllAreas))
        {
            agent.Warp(hit.position); // Move the agent to the NavMesh
            Debug.Log("Agent warped to NavMesh at: " + hit.position);
        }
        else
        {
            Debug.LogError("Agent is not on NavMesh and could not be warped.  Original position: " + transform.position);
            // Consider destroying the agent or GameObject if it can't be placed on the NavMesh
            Destroy(gameObject); //avoid null ref
            return;
        }
    }

    void Update()
    {
        if (agent != null && player != null)
        {
            agent.destination = player.position;
            //agent.SetDestination(playerPos.position);
            transform.LookAt(player);
        }
    }
}