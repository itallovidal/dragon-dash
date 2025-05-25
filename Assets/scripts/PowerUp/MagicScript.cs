using System.Linq;
using UnityEngine;

public class MagicScript : MonoBehaviour
{
    private string[] playerMagics = { "ice_magic", "eletric_magic", "fire_magic" };
    private string magicTag;
    public bool isFromEnemy = false;

    void Start()
    {
        magicTag = gameObject.tag;
    }
    void Update()
    {
        if (magicTag == "eletric_magic")
        {
            float verticalOffset = Random.Range(-4f, 4f);

            Vector3 direction = new Vector3(1f, verticalOffset, 0f);
            transform.position += direction * Time.deltaTime * 4f;
        }
        else if (playerMagics.Contains(magicTag))
        {
            transform.position += Vector3.right * Time.deltaTime * 4f;
        }

        if (isFromEnemy)
        {
            float verticalOffset = Random.Range(-4f, 4f);

            Vector3 direction = new Vector3(-1f, verticalOffset, 0f);
            transform.position += direction * Time.deltaTime * 4f;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!playerMagics.Contains(collision.gameObject.tag))
        {
            Destroy(gameObject);
        }

        if (magicTag == "enemy_power")
        {
            Destroy(gameObject);
        }

        if (collision.gameObject.tag == "center_pipe" && playerMagics.Contains(magicTag))
        {
            Destroy(collision.gameObject);
        }
    }
}
