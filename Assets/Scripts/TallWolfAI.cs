using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class TallWolfAI : MonoBehaviour
{
   AudioManager audioManager;
   //private bool playerPreviouslyDetected = false;
   private bool isChasing = false;

   private Animator enemyAnimator;

   //Enmemy values
   public float health;
   public float damage = 10f;
   public float attackCooldown = 2f;
   private bool canAttack = true;

   public NavMeshAgent agent;
   public Transform player;
   public LayerMask Ground, Player;
   public PlayerManagement pm;

   public GameObject aliveWolf;
   public GameObject deadWolf;
   

   public Vector3 walkPoint;
   bool walkPointSet;
   public float walkPointRange;

   public float sightRange, attackRange;
   public bool playerInSightRange, playerInAttackRange;

   public GameObject deathEffect;
   public ParticleSystem deathParticles;

   //Locating components and player
   private void Awake()
   {
      audioManager = FindObjectOfType<AudioManager>();
		player = GameObject.Find("Player").transform;
		agent = GetComponent<NavMeshAgent>();
      pm = FindObjectOfType<PlayerManagement>();
      enemyAnimator = GetComponent<Animator>();
      
   }

private void Update()
{
   //Sphere check for player in range
    playerInSightRange = Physics.CheckSphere(transform.position, sightRange, Player);
    playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, Player);

   bool playerDetected = playerInSightRange || playerInAttackRange;

    if (!playerDetected)
    {
        if (isChasing)
        {
            audioManager.EndChase();
            isChasing = false;
        }
        Patroling();
    }
    else
    {
        if (!isChasing)
        {
            audioManager.StartChase();
            isChasing = true;
        }
        if (playerInSightRange && !playerInAttackRange)
        {
            ChasePlayer();
        }
        else if (playerInSightRange && playerInAttackRange)
        {
            AttackPlayer();
        }
    }
}

   private void Patroling()
   {
      enemyAnimator.SetBool("TallRunning", false);
      enemyAnimator.SetBool("TallWalking", true);

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

    if (isChasing)
    {
        audioManager.EndChase();
        isChasing = false;
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

   private void ChasePlayer()
   {
      enemyAnimator.SetBool("TallWalking", false);
      enemyAnimator.SetBool("TallRunning", true);
		agent.SetDestination(player.position);
   }

   private void AttackPlayer()
   {
		agent.SetDestination(transform.position);
		//transform.LookAt(player);

      // Attack the player if possible
      if (canAttack)
      {
         PlayerManagement pm = player.GetComponent<PlayerManagement>();
         if (pm != null)
         /*
            DECLAN - In here have it roll between 0 to 1 whent eh enemy is x units away from player.
            if it hits 0.3 or below, call a separate lunch attack function that will launch the 
            enemy with momentum at the player off the ground slightly after first freezing for a second to 
            prepare for the lunge :).

         */
         {
            enemyAnimator.SetTrigger("TallAttack");
            Debug.Log($"{gameObject.name} attacked {player.name}");
            pm.PlayerTakeDamage(damage);
            StartCoroutine(StartAttackCooldown());
         }
      }

   }

   private void OnDrawGizmosSelected()
   {
		  Gizmos.color = Color.red;
		  Gizmos.DrawWireSphere(transform.position, attackRange);
		  Gizmos.color = Color.yellow;
		  Gizmos.DrawWireSphere(transform.position, sightRange);
   }

   // Enemy Damage / Attack Functions
   public void TakeDamage(float amount)
   {
      health -= amount;
      Debug.Log($"{gameObject.name} took {amount} damage, {health} HP left.");

      if (health <= 0)
      {
         StartCoroutine(Die());
      }
   }

   IEnumerator Die()
   {
      audioManager.StartCoroutine(audioManager.EndChase());
      agent.enabled=false;
      aliveWolf.SetActive(false);
      deadWolf.SetActive(true);

      Destroy(Instantiate(deathEffect, transform.position, Quaternion.identity) as GameObject, 2);

      yield return new WaitForSeconds(4f);
      Destroy(gameObject);

   }

   private System.Collections.IEnumerator StartAttackCooldown()
   {
      canAttack = false;
      yield return new WaitForSeconds(attackCooldown);
      canAttack = true;
   }
}
    