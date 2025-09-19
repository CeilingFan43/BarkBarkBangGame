using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SheepAI : MonoBehaviour
{
    public float health;
    public NavMeshAgent agent;
    public Transform player;
    public LayerMask Ground, Player;


    public Vector3 walkPoint;
   bool walkPointSet;
   public float walkPointRange;
    


    private void Awake()
    {
	    player = GameObject.Find("Player").transform;
	    agent = GetComponent<NavMeshAgent>();
    }

    // Start is called before the first frame update


    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    private void Patroling()
   {
		if (!walkPointSet) SearchWalkPoint();

		if (walkPointSet)
      {
		agent.SetDestination(walkPoint);
      }

		Vector3 distanceToWalkPoint = transform.position - walkPoint;

		if (distanceToWalkPoint.magnitude < 1f)
      {
		walkPointSet = false;
      }
   }

    private void SearchWalkPoint()
   {
		  float randomZ = Random.Range(-walkPointRange, walkPointRange);
		  float randomX = Random.Range(-walkPointRange, walkPointRange);

		  walkPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);

		  if (Physics.Raycast(walkPoint, -transform.up, 2f, Ground))
			walkPointSet = true;
   }
}
