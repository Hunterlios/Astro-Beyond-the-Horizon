using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnAsteroids : MonoBehaviour
{
    public GameObject[] asteroidPrefab;
    public Transform player;
    public int maxAsteroids = 100;
    public float spawnRadius = 2000f;
    private int randomNumber;

    private List<GameObject> asteroids = new List<GameObject>();

    void Start()
    {
        asteroids = new List<GameObject>();
        SpawnInitialAsteroids();
    }

    void Update()
    {
        if (asteroids.Count < maxAsteroids)
        {
            SpawnAsteroid();
        }
    }

    void SpawnInitialAsteroids()
    {
        for (int i = 0; i < maxAsteroids; i++)
        {
            SpawnAsteroid();
        }
    }
    void SpawnAsteroid()
    {
        Vector3 playerPosition = player.position;
        Vector3 randomPosition = Random.insideUnitSphere * spawnRadius;
        randomPosition.y = Mathf.Clamp(randomPosition.y, playerPosition.y - spawnRadius, playerPosition.y + spawnRadius);
        Vector3 spawnPosition = playerPosition + randomPosition;

        randomNumber = Random.Range(0, 2);
        GameObject newAsteroid = Instantiate(asteroidPrefab[randomNumber], spawnPosition, Random.rotation);
        Asteroid asteroidComponent = newAsteroid.GetComponent<Asteroid>();
        if (asteroidComponent != null)
        {
            asteroidComponent.OnDestroyed += RemoveAsteroid; 
        }
        asteroids.Add(newAsteroid);
    }

    void RemoveAsteroid(GameObject asteroid)
    {
        asteroids.Remove(asteroid);
    }
}
