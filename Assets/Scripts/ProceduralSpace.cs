using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProceduralSpace : MonoBehaviour
{
    public GameObject player;
    public GameObject asteroidSpawnerPrefab;
    public Vector3 cubeSize = new Vector3(1000, 1000, 1000);
    public float deactivationDistance = 500f;

    private HashSet<Vector3> spawnedCubes = new HashSet<Vector3>();
    private Dictionary<Vector3, GameObject> activeCubes = new Dictionary<Vector3, GameObject>();
    private Vector3 currentCubeCenter = Vector3.zero;
    private int gridSize = 3;

    void Start()
    {
        UpdateSpawnedCubes();
    }

    void Update()
    {
        Vector3 playerPosition = player.transform.position;
        Vector3 playerCubeCenter = new Vector3(
            Mathf.Floor(playerPosition.x / cubeSize.x) * cubeSize.x,
            Mathf.Floor(playerPosition.y / cubeSize.y) * cubeSize.y,
            Mathf.Floor(playerPosition.z / cubeSize.z) * cubeSize.z
        );

        if (!playerCubeCenter.Equals(currentCubeCenter))
        {
            currentCubeCenter = playerCubeCenter;
            UpdateSpawnedCubes();
        }
        DeactivateDistantCubes();
    }

    void UpdateSpawnedCubes()
    {
        Vector3 baseCenter = currentCubeCenter - Vector3.Scale(new Vector3(gridSize, gridSize, gridSize), cubeSize);

        for (int x = 0; x < 2 * gridSize + 1; x++)
        {
            for (int y = 0; y < 2 * gridSize + 1; y++)
            {
                for (int z = 0; z < 2 * gridSize + 1; z++)
                {
                    Vector3 newCubeCenter = baseCenter +  Vector3.Scale(new Vector3(x, y, z), cubeSize);
                    SpawnCube(newCubeCenter);
                }
            }
        }
    }

    void SpawnCube(Vector3 center)
    {
        if (!spawnedCubes.Contains(center))
        {
            GameObject cube = Instantiate(asteroidSpawnerPrefab, center, Quaternion.identity);
            spawnedCubes.Add(center);
            activeCubes.Add(center, cube);
        }
        else if (activeCubes.ContainsKey(center) && !activeCubes[center].activeSelf)
        {
            activeCubes[center].SetActive(true);
        }
    }

    void DeactivateDistantCubes()
    {
        List<Vector3> cubesToDeactivate = new List<Vector3>();

        foreach (var cube in activeCubes)
        {
            if (Vector3.Distance(player.transform.position, cube.Key) > deactivationDistance)
            {
                cube.Value.SetActive(false);
                cubesToDeactivate.Add(cube.Key);
            }
        }

        foreach (var cube in cubesToDeactivate)
        {
            activeCubes.Remove(cube);
        }
    }
}
