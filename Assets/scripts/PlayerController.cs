using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float jumpStrength = 4f;
    public Rigidbody2D myRigidBody;
    float spawnSpeed = 6f;
    LogicManager logicManager;
    bool isAlive = true;
    public Animator animator;
    float rotationSpeed = 4f;

    public GameObject fireball; 

    void Start()
    {
        logicManager = GameObject.FindGameObjectWithTag("Logic").GetComponent<LogicManager>();
    }


    void Update()
    {
        spawn();
        handleMoviment();

    }

    void handleMoviment() {
        // Verificando se o jogador pulou
        if (Input.GetKeyDown(KeyCode.Space) && isAlive)
        {
            myRigidBody.linearVelocity = Vector2.up * jumpStrength;
            animator.SetTrigger("flew");
        }

        // Verificando se utilizou o powerUp
        if (Input.GetKeyDown(KeyCode.UpArrow) && isAlive)
        {
            float spriteHalfWidth = GetComponent<SpriteRenderer>().bounds.extents.x + 0.8f;
            Vector3 dragonPosition = new Vector3(transform.position.x + spriteHalfWidth, transform.position.y, transform.position.z);
            Instantiate(fireball, dragonPosition, Quaternion.Euler(0, 0, 90));
            animator.SetTrigger("triggerFireball");
        }
    }

    void spawn()
    {
        if (transform.position.x < -8)
        {
            transform.position += Vector3.right * spawnSpeed * Time.deltaTime;
        }
        else
        {
            if (spawnSpeed >= 0)
            {
                spawnSpeed -= Time.deltaTime * 5;
                transform.position += Vector3.right * spawnSpeed * Time.deltaTime;
            }
        }
    }

    private void FixedUpdate()
    {
        if ( isAlive )
        {
            transform.rotation = Quaternion.Euler(0, 0, myRigidBody.linearVelocityY * rotationSpeed);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        logicManager.GameOver();
        isAlive = false;
    }
}
