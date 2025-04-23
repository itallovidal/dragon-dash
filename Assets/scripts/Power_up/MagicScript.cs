using System.Linq;
using UnityEngine;

public class MagicScript : MonoBehaviour
{
    private string[] powerupMagics = { "ice_magic", "eletric_magic", "fire_magic" };
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

            if (isFromEnemy)
            {
                Vector3 direction = new Vector3(-1f, verticalOffset, 0f);
                transform.position += direction * Time.deltaTime * 4f;
            }
            else
            {
                Vector3 direction = new Vector3(1f, verticalOffset, 0f);
                transform.position += direction * Time.deltaTime * 4f;
            }

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
