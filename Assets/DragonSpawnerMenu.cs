using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DragonSpawnerMenu : MonoBehaviour
{
    public GameObject[] dragons;

    private float[] positions = { 3.5f, 2f, 0.5f, -1f };
    private List<GameObject> activeDragons = new List<GameObject>();
    private int maxDragons = 4;

    // Update is called once per frame
    void Update()
    {
        if (activeDragons.Count == 0)
        {
            StartCoroutine(SpawnDragons());
        }
    }

    IEnumerator SpawnDragons()
    {
        for (int i = 0; i < maxDragons; i++)
        {
            RandomDragon();
            yield return new WaitForSeconds(1f); // Pequeno intervalo entre os spawns
        }

        yield return new WaitForSeconds(5f); // Tempo para destruir os dragões
        DestroyAllDragons();
    }

    void RandomDragon()
    {
        int dragonRandomIndex = Random.Range(0, dragons.Length);
        GameObject chosenDragon = dragons[dragonRandomIndex];

        int positionsRandomIndex = Random.Range(0, positions.Length);
        Vector3 spawnPosition = new Vector3(-11, positions[positionsRandomIndex], 0);

        GameObject spawnedDragon = Instantiate(chosenDragon, spawnPosition, Quaternion.identity);
        activeDragons.Add(spawnedDragon);
    }

    void DestroyAllDragons()
    {
        foreach (GameObject dragon in activeDragons)
        {
            if (dragon != null)
            {
                Destroy(dragon);
            }
        }
        activeDragons.Clear();
    }
}
