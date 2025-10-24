using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SmallWolfAI : MonoBehaviour
{
   AudioManager audioManager;
   //private bool playerPreviouslyDetected = false;
   private bool isChasing = false;

   private Animator enemyAnimator;

   //Enmemy values
   public float health;
   public float damage = 10f;
   public float attackCooldown = 2f;

   //Pounce attack stuff
   public float pounceRange;
   private bool isPouncing = false;
   private bool hasPounced = false;
   private bool canAttemptPounce = true;
   public float pounceForce;

   private Rigidbody wolfRb;


   private bool canAttack = true;


   public NavMeshAgent agent;
   public Transform player;
   public LayerMask Ground, Player;
   public PlayerManagement pm;
   

   public Vector3 walkPoint;
   bool walkPointSet;
   public float walkPointRange;

   public float sightRange, attackRange;
   public bool playerInSightRange, playerInAttackRange, inPounceRange;

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
      wolfRb = GetComponent<Rigidbody>();
      
   }

private void Update()
{
   //Sphere check for player in range
    playerInSightRange = Physics.CheckSphere(transform.position, sightRange, Player);
    playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, Player);
    inPounceRange = Physics.CheckSphere(transform.position, pounceRange, Player);

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
      
        if (isPouncing)
        {
            return;
        }

        if (inPounceRange && !playerInAttackRange && !hasPounced && canAttemptPounce)
        {
            PounceAttempt();
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
      //enemyAnimator.SetBool("SmallWalking");

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

   private void PounceAttempt()
   {
      canAttemptPounce = false;

      if(canAttack)
      {
        float chance = Random.Range(0f, 1f);
        if (chance <= 0.3f)
        {
            hasPounced = true;
            StartCoroutine(PounceAttack());
        }
        else
        {
            StartCoroutine(PounceAttemptDelay(1f));
            ChasePlayer();
        }
      }
   }

   private IEnumerator PounceAttemptDelay(float delay)
   {
    yield return new WaitForSeconds(delay);
    canAttemptPounce = true;
   }  

   private IEnumerator PounceAttack()
   {
      isPouncing = true;
      canAttack = false;
      agent.isStopped = true;
      agent.enabled = false;
      wolfRb.isKinematic = false;
      //When animation is done, add pounce prep animation

      //if wanting a delayed attack, swap the next two lines so that it jumps to an older position.
      yield return new WaitForSeconds(1f);
      Vector3 direction = (player.position - transform.position).normalized;
      float upForce = 5f;
      
      Vector3 lookDirection = direction;
      lookDirection.y = 0;
      if (lookDirection != Vector3.zero)
      {
        transform.rotation = Quaternion.LookRotation(lookDirection);
      }

      wolfRb.AddForce(direction * pounceForce + Vector3.up * upForce, ForceMode.VelocityChange);
      yield return new WaitForSeconds(0.5f);

      wolfRb.velocity = Vector3.zero;
      wolfRb.isKinematic = true;
      agent.enabled = true;
      agent.isStopped = false;

      if (playerInSightRange && playerInAttackRange)
      {
         AttackPlayer();
      }

      isPouncing = false;
      canAttack = true;
   }

   private void ChasePlayer()
   {
      //enemyAnimator.SetBool("SmallRunning");
		agent.SetDestination(player.position);
   }

   private void AttackPlayer()
   {
		agent.SetDestination(transform.position);
		transform.LookAt(player);

      // Attack the player if possible
      if (canAttack)
      {
         PlayerManagement pm = player.GetComponent<PlayerManagement>();
         if (pm != null)
         {
            //enemyAnimator.SetBool("SmallAttack");
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
      Gizmos.color = Color.green;
		Gizmos.DrawWireSphere(transform.position, pounceRange);
   }

   // Enemy Damage / Attack Functions
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
      audioManager.EndChase();
      //playerPreviouslyDetected = false;
      Destroy(gameObject);
      Destroy(Instantiate(deathEffect, transform.position, Quaternion.identity) as GameObject, 2);
   }

   private System.Collections.IEnumerator StartAttackCooldown()
   {
      canAttack = false;
      yield return new WaitForSeconds(attackCooldown);
      canAttack = true;
   }
}
    