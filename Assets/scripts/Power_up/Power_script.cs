using UnityEngine;

public class PowerScript : MonoBehaviour
{
    void Update()
    {
        transform.position += Vector3.right * Time.deltaTime * 4f;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(gameObject);
    }
}
