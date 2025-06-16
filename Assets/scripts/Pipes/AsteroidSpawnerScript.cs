using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidSpawnerScript : MonoBehaviour
{
    public GameObject asteroid;
    public float minY = -15f;
    public float maxY = 20f;
    public float spawnInterval = 5f;
    public bool isSpawning = true;
    public float delayBetweenPairSpawns = 1f;
    public int maxSpawnAttempts = 10;
    public float asteroidSpawnCheckRadius = 20f;
    public string asteroidTag = "asteroid";

    private List<GameObject> activeAsteroids = new List<GameObject>();
    private float asteroidMoveSpeed = 10f;
    private float asteroidDeadZone = 0f;
    
    void Start()
    {
        InvokeRepeating("TrySpawnAsteroidPair", 0f, spawnInterval);
    }

    private void Update()
    {
        for (int i = activeAsteroids.Count - 1; i >= 0; i--)
        {
            if (activeAsteroids[i] == null)
            {
                activeAsteroids.RemoveAt(i);
                continue;
            }

            GameObject currentActiveAsteroid = activeAsteroids[i];
            currentActiveAsteroid.transform.position += Vector3.left * Time.deltaTime * asteroidMoveSpeed;

            if (currentActiveAsteroid.transform.position.x <= asteroidDeadZone)
            {
                Destroy(currentActiveAsteroid);
                activeAsteroids.RemoveAt(i);
            }
        }
    }

    void TrySpawnAsteroidPair()
    {
        if (!isSpawning || asteroid == null)
        {
            if (asteroid == null && isSpawning)
            {
                Debug.LogError("AsteroidSpawnerScript: Asteroid prefab not assigned in Inspector.");
            }
            return;
        }
        StartCoroutine(SpawnTwoAsteroidsWithCheck());
    }

    IEnumerator SpawnTwoAsteroidsWithCheck()
    {
        float halfCameraHeight = Camera.main.orthographicSize;
        float halfCameraWidth = halfCameraHeight * Camera.main.aspect;
        float cameraEdgeX = Camera.main.transform.position.x;

        asteroidDeadZone = cameraEdgeX - halfCameraWidth - asteroidSpawnCheckRadius;
        float spawnX = cameraEdgeX + halfCameraWidth + asteroidSpawnCheckRadius;

        for (int i = 0; i < 2; i++)
        {
            bool positionFound = false;
            Vector3 spawnPosition = Vector3.zero;

            for (int attempt = 0; attempt < maxSpawnAttempts; attempt++)
            {
                float randomY = UnityEngine.Random.Range(minY, maxY);
                Vector3 candidatePosition = new Vector3(spawnX, randomY, 0);

                if (IsPositionSafe(candidatePosition, asteroidSpawnCheckRadius))
                {
                    spawnPosition = candidatePosition;
                    positionFound = true;
                    break;
                }
            }

            if (positionFound)
            {
                GameObject asteroidInstance = Instantiate(asteroid, spawnPosition, Quaternion.identity);
                float heightMultiplier = UnityEngine.Random.Range(0.5f, 1.5f);
                asteroidInstance.transform.localScale *= heightMultiplier;

                activeAsteroids.Add(asteroidInstance);
            }
            else
            {
                Debug.LogWarning($"AsteroidSpawnerScript: Could not find a safe position to spawn asteroid {i + 1} after {maxSpawnAttempts} attempts.");
            }

            if (i == 0)
            {
                yield return new WaitForSeconds(delayBetweenPairSpawns);
            }
        }
    }

    bool IsPositionSafe(Vector3 candidatePosition, float radius)
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(candidatePosition, radius);
        foreach (Collider2D col in colliders)
        {

            if (col.gameObject.CompareTag(asteroidTag) ||
                col.gameObject.CompareTag("eletric_power_up") ||
                col.gameObject.CompareTag("fire_power_up") ||
                col.gameObject.CompareTag("ice_power_up"))
            {
                return false;
            }
        }
        return true;
    }
}
