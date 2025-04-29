using UnityEngine;

public class EnemyLogic : MonoBehaviour
{
    // bool isAlive = true;
    public GameObject enemyPower;
    public float attackInterval = 2f;
    public GameObject enemyDeathPrefab;

    private void Start()
    {
        InvokeRepeating("Attack", 0f, attackInterval);
    }

    void Attack()
    {
        // instanciando o elemento um pouco mais a frente do drag�o
        float spriteHalfWidth = GetComponent<SpriteRenderer>().bounds.extents.x + 0.8f;
        Vector3 powerPositon = new Vector3(transform.position.x - spriteHalfWidth, transform.position.y, transform.position.z);
        GameObject power = Instantiate(enemyPower, powerPositon, Quaternion.Euler(0, 0, 90));
        power.GetComponent<MagicScript>().isFromEnemy = true;

        // Destroi o poder disparado pelo dragao depois de 3.5 segundos
        Destroy(power, 3.5f);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        switch (collision.gameObject.tag)
        {
            case "Player":
            case "fire_magic":
            case "ice_magic":
            case "eletric_magic":
                TriggerDeathAnimation();
                Destroy(gameObject);
                break;
        }
    }

    void TriggerDeathAnimation()
    {
        if (enemyDeathPrefab != null)
        {
            // Instancia o prefab de explosão na posição do inimigo
            GameObject explosion = Instantiate(enemyDeathPrefab, transform.position, Quaternion.identity);
        }
    }
}