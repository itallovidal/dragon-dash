using UnityEngine;

public class FireballScript : MonoBehaviour
{
    void Start()
    {
        
    }

    void Update()
    {
        // Lancando para frente a fireball
        transform.position += Vector3.right * Time.deltaTime * 4f;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(gameObject);
    }
}
