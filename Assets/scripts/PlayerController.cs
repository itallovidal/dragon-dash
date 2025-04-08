using NUnit.Framework.Constraints;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public enum DragonPower
    {
        FIRE_POWER,
        ICE_POWER,
        ELETRIC_POWER,
        STARNDARD_POWER
    }

    public Rigidbody2D dragonRigidBody;
    bool isAlive = true;
    public DragonPower currentPower = DragonPower.STARNDARD_POWER;

    private float dragonSpeed = 6f;
    // Rota��o do drag�o em rela��o a sua velocidade
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
        //animator.enabled = false;
        ApplyInitialPower();
    }


    void Update()
    {
        handleDragonFlySpeed();
        handleMoviment();
    }

    void handleMoviment()
    {
        // Verificando se o jogador pulou
        if (Input.GetKeyDown(KeyCode.Space) && isAlive)
        {
            handleFly();
        }

        // Verificando se utilizou o powerUp
        if (Input.GetKeyDown(KeyCode.UpArrow) && isAlive)
        {
            switch (currentPower)
            {
                case DragonPower.FIRE_POWER:
                    attack(fireball);
                    break;
                case DragonPower.ICE_POWER:
                    attack(iceball);
                    break;
                case DragonPower.ELETRIC_POWER:
                    attack(electricball);
                    break;
                default:
                    break;
            }
        }
    }

    void attack(GameObject spriteAttack)
    {
        float spriteHalfWidth = GetComponent<SpriteRenderer>().bounds.extents.x + 0.8f;
        Vector3 dragonPosition = new Vector3(transform.position.x + spriteHalfWidth, transform.position.y, transform.position.z);
        GameObject element = Instantiate(spriteAttack, dragonPosition, Quaternion.Euler(0, 0, 90));
        Destroy(element, 3.5f);
    }

    void handleDragonFlySpeed()
    {
        // Verificando se o drag�o j� entrou em tela
        // Se ele estiver fora da tela, a velocidade ser� maior
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
        transform.rotation = Quaternion.Euler(0, 0, dragonRigidBody.linearVelocity.y * dragonRotation);
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

        switch (collision.tag)
        {
            case "Ice":
                ChangePower(DragonPower.ICE_POWER, collision.gameObject);
                break;
            case "Eletric":
                ChangePower(DragonPower.ELETRIC_POWER, collision.gameObject);
                break;
            case "Fire":
                ChangePower(DragonPower.FIRE_POWER, collision.gameObject);
                break;
            default:
                break;
        }
    }

    void handleFly()
    {
        //switch (currentPower)
        //{
        //    case DragonPower.ICE_POWER:
        //        break;
        //    case DragonPower.ELETRIC_POWER:
        //        animator.SetTrigger("flyEletricDragon");
        //        animator.SetBool("isEletric", true);
        //        break;  
        //    case DragonPower.FIRE_POWER:
        //        animator.SetTrigger("flyFireDragon");
        //        break;

        //    case DragonPower.STARNDARD_POWER:
        //        animator.SetTrigger("flyStandardDragon");
        //        animator.SetBool("isNoPower", true);
        //        break;
        //}

        animator.SetTrigger("flew");
        dragonRigidBody.linearVelocity = Vector2.up * jumpStrength;
    }

    string getPower(DragonPower power)
    {

        return power switch
        {
            DragonPower.FIRE_POWER => "FIRE_POWER",
            DragonPower.ICE_POWER => "ICE_POWER",
            DragonPower.ELETRIC_POWER => "ELETRIC_POWER",
            DragonPower.STARNDARD_POWER => "Standard",
            _ => "Unknown"
        };
    }

    void resetPowers()
    {
        animator.SetBool("hasPower", false);
        animator.SetBool("ELETRIC_POWER", false);
        animator.SetBool("ICE_POWER", false);
        animator.SetBool("FIRE_POWER", false);

    }

    void ChangePower(DragonPower newPower, GameObject powerUp)
    {
        resetPowers();
        currentPower = newPower;
        if(newPower != DragonPower.STARNDARD_POWER)
        {
            animator.SetBool("hasPower", false);
        }

        animator.SetBool("hasPower", true);
        string power = getPower(newPower);
        animator.SetBool(power, true);

        if (powerUp)
        {
            Destroy(powerUp);
        }
    }

    void ApplyInitialPower() {
        ChangePower(currentPower, null);
    }
}
