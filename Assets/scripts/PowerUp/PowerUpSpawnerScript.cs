using System;
using System.Collections;
using System.Linq;
using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;

public class PowerUpSpawnerScript : MonoBehaviour
{
    public GameObject[] powerUps;  // Arrasta os PowerUps aqui no Inspector
    public float minY = -2f;
    public float maxY = 2f;
    public float spawnInterval = 5f; 
    public Boolean isSpawning = true;

    private List<GameObject> activePowersUps = new List<GameObject>();
    private float powerUpMoveSpeed = 10f;
    private float powerUpDeadZone = 0f;

    void Start()
    {
        // irá invocar a função "SpawnRandomPowerUp" a cada "spawnInterval" segundos
        InvokeRepeating("SpawnRandomPowerUp", 0f, spawnInterval);
    }

    private void Update()
    {
        if(activePowersUps != null || activePowersUps.Count > 0)
        {
            // esse laço deve ser invertido pois ao retirar 
            // um elemento da pilha, não devemos mudar a ordem 
            // dos elementos não afetados
            for (int i = activePowersUps.Count - 1; i >= 0 ; i--)
            {
                // temos que verificar se o elemento atual
                // ainda existe ou foi destruído por outra causa
                if(activePowersUps[i] == null)
                {
                    activePowersUps.RemoveAt(i);
                    continue;
                }

                GameObject activerPowerUp = activePowersUps[i];

                // Com o power up atual da lista, o pegamos e o movemos para a deadzone
                // Caso ele chegue a deadzone, destruímos esse power up e o removemos da lista
                activerPowerUp.transform.position += Vector3.left * Time.deltaTime * powerUpMoveSpeed;
                if (activerPowerUp.transform.position.x <= powerUpDeadZone)
                {
                    Destroy(activerPowerUp);
                    activePowersUps.RemoveAt(i);
                }
            }
        }
    }

    void SpawnRandomPowerUp()
    {
        if (!isSpawning) return;

        if (powerUps == null || powerUps.Length == 0)
        {
            Debug.Log("Power ups ainda não carregados..");
            return;
        }

        // Pegando um power up aleatório
        int randomIndex = UnityEngine.Random.Range(0, powerUps.Length);
        GameObject chosenPowerUp = powerUps[randomIndex];

        // Definindo variáveis de spawn e despawn
        float halfCameraWidth = Camera.main.orthographicSize * Camera.main.aspect;
        float cameraRightEdge = Camera.main.transform.position.x + halfCameraWidth;
        powerUpDeadZone = Camera.main.transform.position.x - halfCameraWidth;

        // Definindo em qual posição iremos spawnar o power up
        float randomY = UnityEngine.Random.Range(minY, maxY);
        Vector3 spawnPosition = new Vector3(cameraRightEdge * 1.05f, randomY, 0);

        // Spawnando o power up e adicionando na lista de power ups em tela
        GameObject powerUpInstance = Instantiate(chosenPowerUp, spawnPosition, Quaternion.identity);
        activePowersUps.Add(powerUpInstance);
    }
}
