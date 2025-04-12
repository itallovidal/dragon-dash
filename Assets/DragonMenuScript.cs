using UnityEngine;

public class DragonMenuScript : MonoBehaviour
{

    private float dragonSpeed;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        dragonSpeed = Random.Range(5f, 6f);
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.x > 10)
        {
            Destroy(gameObject);
            return;
        }

        transform.position += Vector3.right * dragonSpeed * Time.deltaTime;
    }
}
