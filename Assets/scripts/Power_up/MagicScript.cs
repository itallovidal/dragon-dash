using System.Linq;
using UnityEngine;

public class MagicScript : MonoBehaviour
{
    private string[] powerupMagics = { "ice_magic", "eletric_magic", "fire_magic" };
    void Update()
    {
        transform.position += Vector3.right * Time.deltaTime * 4f;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!powerupMagics.Contains(collision.gameObject.tag))
        {
            Destroy(gameObject);
        }
    }
}
