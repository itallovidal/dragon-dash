using System;
using System.Linq;
using NUnit.Framework.Constraints;
using UnityEngine;

public class PlayerScript : MonoBehaviour
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
    public DragonPower currentPower;

    private float dragonSpeed = 6f;
    private float dragonRotation = 2f;
    public float jumpStrength = 4f;

    GameLogic gameLogic;
    public Animator animator;

    // GameObjects de Power Up
    public GameObject fireball;
    public GameObject iceball;
    public GameObject electricball;


    void Start()
    {
        gameLogic = GameObject.FindGameObjectWithTag("Logic").GetComponent<GameLogic>();
        ChangePower(currentPower);
    }


    void Update()
    {
        handleDragonFlySpeed();
        handleMoviment();
    }


    private void FixedUpdate()
    {
        // Rotacionando baseado na sua velocidade
        transform.rotation = Quaternion.Euler(0, 0, dragonRigidBody.linearVelocity.y * dragonRotation);
    }

    // Se ele colidir com qualquer coisa é game over!
    private void OnCollisionEnter2D(Collision2D collision)
    {
        gameLogic.GameOver();
        isAlive = false;
    }

    // Se ele colidir com algo que é um trigger, depende do que esse algo é
    // pode ser uma moeda, um power up ou um score space
    private void OnTriggerEnter2D(Collider2D collision)
    {
        string[] tags = { "ice_power_up", "eletric_power_up", "fire_power_up" };
        switch (collision.tag)
        {
            case "ice_power_up":
                ChangePower(DragonPower.ICE_POWER);
                break;
            case "eletric_power_up":
                ChangePower(DragonPower.ELETRIC_POWER);
                break;
            case "fire_power_up":
                ChangePower(DragonPower.FIRE_POWER);
                break;
            default:
                break;
        }

        if (tags.Contains(collision.tag))
        {
            Destroy(collision.gameObject);
        }

        // Se colidir com o trigger do SceneCollider chame o GameOver
        if (collision.gameObject.layer == 6)
        {
            gameLogic.GameOver();
            isAlive = false;
        }

    }

    void handleMoviment()
    {
        // Verificando se o jogador pulou
        if (Input.GetKeyDown(KeyCode.Space) && isAlive)
        {
            animator.SetTrigger("flew");
            dragonRigidBody.linearVelocity = Vector2.up * jumpStrength;
        }

        // Verificando se utilizou o powerUp
        if (Input.GetKeyDown(KeyCode.UpArrow) && isAlive)
        {
            animator.SetTrigger("attack");
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
        // instanciando o elemento um pouco mais a frente do dragão
        float spriteHalfWidth = GetComponent<SpriteRenderer>().bounds.extents.x + 0.8f;
        Vector3 dragonPosition = new Vector3(transform.position.x + spriteHalfWidth, transform.position.y, transform.position.z);
        GameObject power = Instantiate(spriteAttack, dragonPosition, Quaternion.Euler(0, 0, 90));

        // Destroi o poder disparado pelo dragao depois de 3.5 segundos
        Destroy(power, 3.5f);
    }

    void handleDragonFlySpeed()
    {
        if (transform.position.x < -8)
        {
            transform.position += Vector3.right * dragonSpeed * Time.deltaTime;
            return;
        }

        dragonSpeed = Mathf.Max(0, dragonSpeed - Time.deltaTime * 5);
        transform.position += Vector3.right * dragonSpeed * Time.deltaTime;
    }

    string getPowerName(DragonPower power)
    {
        return power switch
        {
            DragonPower.FIRE_POWER => "FIRE_POWER",
            DragonPower.ICE_POWER => "ICE_POWER",
            DragonPower.ELETRIC_POWER => "ELETRIC_POWER",
            _ => "_"
        };
    }

    void resetAnimationState()
    {
        animator.SetBool("hasPower", false);
        animator.SetBool("ELETRIC_POWER", false);
        animator.SetBool("ICE_POWER", false);
        animator.SetBool("FIRE_POWER", false);
    }

    void ChangePower(DragonPower newPower)
    {
        resetAnimationState();
        currentPower = newPower;

        if (newPower == DragonPower.STARNDARD_POWER)
        {
            animator.SetBool("hasPower", false);
            return;
        }

        animator.SetBool("hasPower", true);
        animator.SetBool(getPowerName(newPower), true);

        if (currentPower != DragonPower.STARNDARD_POWER)
        {
            // Redefini as chamadas do Invoke
            CancelInvoke("ResetPowers");
            Invoke("ResetPowers", 5f);
        }
    }
    void ResetPowers()
    {
        ChangePower(DragonPower.STARNDARD_POWER);
    }
}
