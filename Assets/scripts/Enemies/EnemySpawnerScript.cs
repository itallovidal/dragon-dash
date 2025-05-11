using System;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnerScript : MonoBehaviour
{
    public GameObject enemy; 
    public float minY = -4f;
    public float maxY = 4f;
    public float spawnInterval = 3f;
    public Boolean isSpawning = true;

    private List<GameObject> activeEnemies = new List<GameObject>();
    private float enemyMoveSpeed = 3f;
    private float enemyDeadZone = 0f;

    private void Start()
    {
        InvokeRepeating("SpawnEnemy", 0f, spawnInterval);
    }

    private void Update()
    {
        if (activeEnemies != null || activeEnemies.Count > 0)
        {
            // esse laço deve ser invertido pois ao retirar 
            // um elemento da pilha, não devemos mudar a ordem 
            // dos elementos não afetados
            for (int i = activeEnemies.Count - 1; i >= 0; i--)
            {
                // temos que verificar se o elemento atual
                // ainda existe ou foi destruído por outra causa
                if (activeEnemies[i] == null)
                {
                    activeEnemies.RemoveAt(i);
                    continue;
                }

                GameObject currentEnemy = activeEnemies[i];

                // Com o inimigo atual da lista, o pegamos e o movemos para a deadzone
                // Caso ele chegue a deadzone, destruímos esse inimigo e o removemos da lista
                currentEnemy.transform.position += Vector3.left * Time.deltaTime * enemyMoveSpeed;
                if (currentEnemy.transform.position.x <= enemyDeadZone)
                {
                    Destroy(currentEnemy);
                    activeEnemies.RemoveAt(i);
                }
            }
        }
    }

    private void SpawnEnemy()
    {
        if (!isSpawning) return;

        if (enemy == null)
        {
            Debug.Log("Inimigos ainda não carregados..");
            return;
        }


        // Definindo variáveis de spawn e despawn
        float halfCameraWidth = Camera.main.orthographicSize * Camera.main.aspect;
        float cameraRightEdge = Camera.main.transform.position.x + halfCameraWidth;
        enemyDeadZone = Camera.main.transform.position.x - halfCameraWidth;

        // Definindo em qual posição iremos spawnar o inimigo
        float randomY = UnityEngine.Random.Range(minY, maxY);
        Vector3 spawnPosition = new Vector3(cameraRightEdge * 1.05f, randomY, 0);

        Debug.Log("Spawnando inimigo...");
        // Spawnando o inimigo 
        GameObject enemyInstance = Instantiate(enemy, spawnPosition, Quaternion.identity);
        activeEnemies.Add(enemyInstance);
    }
}
