using UnityEngine;

public class PlayerController : MonoBehaviour
{
    enum DragonPower
    {
        FIRE_POWER,
        ICE_POWER,
        ELETRIC_POWER,
        STARNDARD_POWER
    }

    public Rigidbody2D dragonRigidBody;
    bool isAlive = true;
    DragonPower currentPower = DragonPower.STARNDARD_POWER;

 
    private float dragonSpeed = 6f;
    // Rotação do dragão em relação a sua velocidade
    private float dragonRotation = 2f;
    public float jumpStrength = 4f;

    LogicManager logicManager;
    public Animator animator;

    // GameObjects de Power Up
    public GameObject fireball; 
    public GameObject iceball;
    public GameObject electricball;


    void Start()
    {
        logicManager = GameObject.FindGameObjectWithTag("Logic").GetComponent<LogicManager>();
    }


    void Update()
    {
        handleDragonFlySpeed();
        handleMoviment();
    }

    void handleMoviment() {
        // Verificando se o jogador pulou
        if (Input.GetKeyDown(KeyCode.Space) && isAlive)
        {
            dragonRigidBody.linearVelocity = Vector2.up * jumpStrength;
            animator.SetTrigger("flew");
        }

        // Verificando se utilizou o powerUp
        if (Input.GetKeyDown(KeyCode.UpArrow) && isAlive)
        {
            switch (currentPower)
            {
                case DragonPower.FIRE_POWER:
                    attack(fireball, "triggerFireball");
                    break;
                case DragonPower.ICE_POWER:
                    attack(iceball, "triggerIceball");
                    break;
                case DragonPower.ELETRIC_POWER:
                    attack(electricball, "triggerEletricball");
                    break;
                default:
                    break;
            }
        }
    }

    void attack(GameObject spriteAttack, string animationTriggerName)
    {
        float spriteHalfWidth = GetComponent<SpriteRenderer>().bounds.extents.x + 0.8f;
        Vector3 dragonPosition = new Vector3(transform.position.x + spriteHalfWidth, transform.position.y, transform.position.z);
        Instantiate(spriteAttack, dragonPosition, Quaternion.Euler(0, 0, 90));
        animator.SetTrigger(animationTriggerName);
    }

    void handleDragonFlySpeed()
    {
        // Verificando se o dragão já entrou em tela
        // Se ele estiver fora da tela, a velocidade será maior
        if (transform.position.x < -8)
        {
            transform.position += Vector3.right * dragonSpeed * Time.deltaTime;
        }
        else
        {
            if (dragonSpeed >= 0)
            {
                dragonSpeed -= Time.deltaTime * 5;
                transform.position += Vector3.right * dragonSpeed * Time.deltaTime;
            }
        }
    }

    private void FixedUpdate()
    {
        // Rotacionando baseado na sua velocidade
        transform.rotation = Quaternion.Euler(0, 0, dragonRigidBody.linearVelocityY * dragonRotation);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        logicManager.GameOver();
        isAlive = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 6)
        {
            logicManager.GameOver();
            isAlive = false;
        }
    }
}
