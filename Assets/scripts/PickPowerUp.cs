using UnityEngine;

public class PickPowerUp : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Fire"))
        {
            Destroy(col.gameObject);
        }
        else if (col.CompareTag("Eletric"))
        {
            Destroy(col.gameObject);
        }
    }
}
