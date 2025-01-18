using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemies : MonoBehaviour
{
    public GameObject enemyPrefab;
    public Transform player;
    public int maxEnemies = 20;
    public float spawnRadius = 1000f;

    private List<GameObject> enemies = new List<GameObject>();

    void Start()
    {
        enemies = new List<GameObject>();
        SpawnInitialEnemies();
    }

    void Update()
    {
        if (enemies.Count < maxEnemies)
        {
            SpawnEnemy();
        }
    }

    void SpawnInitialEnemies()
    {
        for (int i = 0; i < maxEnemies; i++)
        {
            SpawnEnemy();
        }
    }
    void SpawnEnemy()
    {
        Vector3 playerPosition = player.position;
        Vector3 randomPosition = Random.insideUnitSphere * spawnRadius;
        randomPosition.y = Mathf.Clamp(randomPosition.y, playerPosition.y - spawnRadius, playerPosition.y + spawnRadius);
        Vector3 spawnPosition = playerPosition + randomPosition;

        Debug.Log($"Spawning enemy at position: {spawnPosition}");

        GameObject newEnemy = Instantiate(enemyPrefab, spawnPosition, Random.rotation);
        Enemy enemyComponent = newEnemy.GetComponent<Enemy>();
        if (enemyComponent != null)
        {
            enemyComponent.OnDestroyed += RemoveEnemies;
        }
        enemies.Add(newEnemy);
    }

    void RemoveEnemies(GameObject enemy)
    {
        enemies.Remove(enemy);
    }
}
