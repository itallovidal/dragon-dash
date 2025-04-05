using UnityEngine;

public class PipeScript : MonoBehaviour
{
    float deadZone = -15;
    public float moveSpeed = 2;
    void Start()
    {
    }

    void Update()
    {
        transform.position += Vector3.left * Time.deltaTime * moveSpeed;

        DespawnPipe();
    }

    private void DespawnPipe()
    {
        if (transform.position.x <= deadZone)
        {
            Destroy(gameObject);
        }
    }
}
