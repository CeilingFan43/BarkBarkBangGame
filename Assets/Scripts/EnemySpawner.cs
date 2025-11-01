using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public float health = 150f;

    public GameObject wolfPrefab;
    public GameObject quadWolfPrefab;
    public float initialSpawnRate = 6f;
    public float spawnRateIncreaseInterval = 60f;
    public float spawnRateIncreaseAmount = 2f;
    public float maxSpawnRate = 15f;

    private float timeElapsed = 0f;
    private float currentSpawnRate;
    private float nextSpawnTime;

     public float tallWolfSpawnChance = 0.7f;

    // To track when the spawn rate was last increased
    private float lastIncreaseTime = 0f; 
    
    public float spawnRadius = 50f;

    void Start()
    {
        currentSpawnRate = initialSpawnRate / 60f;
        SetNextSpawnTime();
    }

    //Wolf spawn scaling
    void Update()
    {
        timeElapsed += Time.deltaTime;

        if (timeElapsed >= nextSpawnTime)
        {
            SpawnWolf();
            SetNextSpawnTime();
        }

        // Check if it's time to increase the spawn rate and if it hasn't been increased recently
        if (timeElapsed >= lastIncreaseTime + spawnRateIncreaseInterval)
        {
            IncreaseSpawnRate();
            lastIncreaseTime = timeElapsed; // Update the last increase time
        }
    }

    //Wolf spawning 
    void SpawnWolf()
    {
        float roll = Random.value;

        Vector3 randomSpawnPosition = transform.position + new Vector3(Random.Range(-spawnRadius, spawnRadius), 0f, Random.Range(-spawnRadius, spawnRadius));

        if (roll < tallWolfSpawnChance)
        {
            // Spawn normal wolf
            Instantiate(wolfPrefab, randomSpawnPosition, Quaternion.identity);

        }
        else
        {
            // Spawn quad wolf
            Instantiate(quadWolfPrefab, randomSpawnPosition, Quaternion.identity);

        }
    }

    void SetNextSpawnTime()
    {
        nextSpawnTime = timeElapsed + (1f / currentSpawnRate);
    }

    void IncreaseSpawnRate()
    {
        currentSpawnRate = Mathf.Min(currentSpawnRate + (spawnRateIncreaseAmount / 60f), maxSpawnRate / 60f);
        Debug.Log("Spawn rate increased to: " + currentSpawnRate * 60f + " wolves per minute.");
    }

    //Gizmos for spawns
    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, spawnRadius);
    }

    //EnemySpawner damage function
    public void TakeDamage(float amount)
   {
      health -= amount;
      Debug.Log($"{gameObject.name} took {amount} damage, {health} HP left.");

      if (health <= 0)
      {
         Die();
      }
   }

    //EnemSpawner destroy
    void Die()
   {
      Debug.Log($"{gameObject.name} died.");
      Destroy(gameObject);
   }
}