using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public float health = 150f;

    public GameObject wolfPrefab;
    public float initialSpawnRate = 6f;
    public float spawnRateIncreaseInterval = 60f;
    public float spawnRateIncreaseAmount = 2f;
    public float maxSpawnRate = 15f;

    private float timeElapsed = 0f;
    private float currentSpawnRate;
    private float nextSpawnTime;
    private float lastIncreaseTime = 0f; // To track when the spawn rate was last increased
    public float spawnRadius = 50f;

    void Start()
    {
        currentSpawnRate = initialSpawnRate / 60f;
        SetNextSpawnTime();
    }

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

    void SpawnWolf()
    {
        Vector3 randomSpawnPosition = transform.position + new Vector3(Random.Range(-spawnRadius, spawnRadius), 0f, Random.Range(-spawnRadius, spawnRadius));
        Instantiate(wolfPrefab, randomSpawnPosition, Quaternion.identity);
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

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red; // Set the color of the gizmo
        Gizmos.DrawWireSphere(transform.position, spawnRadius); // Draw a wireframe sphere
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