using System.Linq;
using UnityEngine;

public class MagicScript : MonoBehaviour
{
    private string[] powerupMagics = { "ice_magic", "eletric_magic", "fire_magic" };
    private string magicTag;

    void Start()
    {
        magicTag = gameObject.tag;
    }
    void Update()
    {
        Debug.Log("tag");
        Debug.Log(magicTag);
        if (magicTag == "eletric_magic")
        {
            float verticalOffset = Random.Range(-4f,4f);
            Vector3 direction = new Vector3(1f, verticalOffset, 0f);
            transform.position += direction * Time.deltaTime * 4f;
        }
        else
        {
            transform.position += Vector3.right * Time.deltaTime * 4f;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!powerupMagics.Contains(collision.gameObject.tag))
        {
            Destroy(gameObject);
        }
    }
}
