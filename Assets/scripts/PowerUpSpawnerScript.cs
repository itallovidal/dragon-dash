using System.Collections;
using UnityEngine;

public class PowerUpSpawnerScript : MonoBehaviour
{
    public GameObject[] powerUps;  // Arrasta os PowerUps aqui no Inspector
    public float minY = -2f;
    public float maxY = 2f;
    public float spawnInterval = 5f; // Tempo entre os spawns

    private bool canSpawn = true;

    void Start()
    {
        StartCoroutine(SpawnPowerUpLoop());
    }

    IEnumerator SpawnPowerUpLoop()
    {
        while (true)
        {
            if (canSpawn)
            {
                SpawnRandomPowerUp();
                canSpawn = false;
                yield return new WaitForSeconds(spawnInterval); // Aguarda o tempo definido antes do próximo spawn
                canSpawn = true;
            }

            yield return null;
        }
    }

    void SpawnRandomPowerUp()
    {
        Debug.Log("Spawnando PowerUp...");
        int randomIndex = Random.Range(0, powerUps.Length);
        GameObject chosenPowerUp = powerUps[randomIndex];
        Debug.Log($"PowerUp escolhido: {randomIndex}");

        float cameraRightEdge = Camera.main.transform.position.x + Camera.main.orthographicSize * Camera.main.aspect;
        float spriteHalfWidth = chosenPowerUp.GetComponent<SpriteRenderer>().bounds.extents.x;
        float randomY = Random.Range(minY, maxY);

        Vector3 spawnPosition = new Vector3(cameraRightEdge + spriteHalfWidth, randomY, 0);

        GameObject instance = Instantiate(chosenPowerUp, spawnPosition, Quaternion.identity);

        setResetPlace(instance, cameraRightEdge + spriteHalfWidth);
        setPowerUpMovespeed(instance, 0.5f);
        setDeadZone(instance, cameraRightEdge + spriteHalfWidth);
    }

    // public void OnPowerUpCollected()
    // {
    //     StartCoroutine(HandlePowerUpEffect());
    // }

    // IEnumerator HandlePowerUpEffect()
    // {
    //     Debug.Log("PowerUp ativado!");
    //     yield return new WaitForSeconds(2f); // Tempo com efeito ativo
    //     Debug.Log("PowerUp acabou!");
    //     yield return new WaitForSeconds(2f); // Tempo até o próximo spawn
    //     canSpawn = true;
    // }

    void setDeadZone(GameObject PowerUpSprite, float deadZone)
    {
        PowerUpScript script = PowerUpSprite.GetComponent<PowerUpScript>();
        if (script != null) script.deadZone = deadZone;
    }

    void setPowerUpMovespeed(GameObject PowerUpSprite, float speed)
    {
        PowerUpScript script = PowerUpSprite.GetComponent<PowerUpScript>();
        if (script != null) script.moveSpeed = speed;
    }

    void setResetPlace(GameObject PowerUpSprite, float resetPlace)
    {
        PowerUpScript script = PowerUpSprite.GetComponent<PowerUpScript>();
        if (script != null) script.powerUpResetPlace = resetPlace;
    }
}
