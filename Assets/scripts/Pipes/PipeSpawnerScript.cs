using System.Threading;
using UnityEngine;

public class PipeSpawnerScript : MonoBehaviour
{
    public GameObject pipe;

    public float spawnRate = 2;
    float timer = 0;

    float offsetHeight = 3.5f;
    void Start()
    {
        SpawnPipe();
    }

    void Update()
    {
        if(timer < spawnRate)
        {
            timer += Time.deltaTime;
        }
        else
        {
            SpawnPipe();
            timer = 0;
        }
    }

    private void SpawnPipe()
    {
        float highestPoint = transform.position.y + offsetHeight;
        float lowestPoint = transform.position.y - offsetHeight;
        float randomPoint = Random.Range(lowestPoint, highestPoint);

        float cameraBounds = Camera.main.transform.position.x + Camera.main.orthographicSize * Camera.main.aspect + 3;

        Vector3 randomVector = new Vector3(cameraBounds, randomPoint);

        Instantiate(pipe, randomVector, transform.rotation);
    }
}
