using UnityEngine;

public class DragonSpawnerMenu : MonoBehaviour
{

    private float dragonSpeed = 6f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
     if (transform.position.x < -8)
        {
            transform.position += Vector3.right * dragonSpeed * Time.deltaTime;
            return;
        }

        dragonSpeed = Mathf.Max(0, dragonSpeed - Time.deltaTime * 5);
        transform.position += Vector3.right * dragonSpeed * Time.deltaTime;   
    }
}
