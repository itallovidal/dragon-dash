using UnityEngine;

public class PlayerController : MonoBehaviour
{
    enum DragonPower
    {
        FIRE_POWER,
        ICE_POWER,
        ELETRIC_POWER,
        STANDARD_POWER
    }

    [Header("Dragão")]
    public Rigidbody2D dragonRigidBody;
    public SpriteRenderer spriteRenderer;
    public Sprite standardDragonSprite;
    public Sprite iceDragonSprite;
    public Sprite fireDragonSprite;
    public Sprite eletricDragonSprite;

    [Header("Animator")]
    public Animator animator;
    public AnimationClip idleStandard;
    public AnimationClip flyStandard;
    public AnimationClip idleFire;
    public AnimationClip flyFire;
    public AnimationClip idleIce;
    public AnimationClip flyIce;
    public AnimationClip idleEletric;
    public AnimationClip flyEletric;

    private AnimatorOverrideController animatorOverride;

    [Header("PowerUps")]
    public GameObject fireball;
    public GameObject iceball;
    public GameObject electricball;

    [Header("Outros")]
    public float dragonSpeed = 6f;
    public float dragonRotation = 2f;
    public float jumpStrength = 4f;

    private bool isAlive = true;
    private DragonPower currentPower = DragonPower.STANDARD_POWER;
    private LogicManager logicManager;


    void Start()
    {
        logicManager = GameObject.FindGameObjectWithTag("Logic").GetComponent<LogicManager>();
        animatorOverride = new AnimatorOverrideController(animator.runtimeAnimatorController);
        animator.runtimeAnimatorController = animatorOverride;

        ChangePower(DragonPower.STANDARD_POWER);
    }

    void Update()
    {
        handleDragonFlySpeed();
        handleMoviment();
    }

    void handleMoviment()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isAlive)
        {
            handleFly();
        }

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
            }
        }
    }

    void handleFly()
    {
        dragonRigidBody.linearVelocity = Vector2.up * jumpStrength;
        animator.SetTrigger("fly");
    }

    void attack(GameObject spriteAttack, string animationTriggerName)
    {
        float spriteHalfWidth = spriteRenderer.bounds.extents.x + 0.8f;
        Vector3 dragonPosition = new Vector3(transform.position.x + spriteHalfWidth, transform.position.y, transform.position.z);
        Instantiate(spriteAttack, dragonPosition, Quaternion.Euler(0, 0, 90));
        animator.SetTrigger(animationTriggerName);
    }

    void handleDragonFlySpeed()
    {
        if (transform.position.x < -8)
        {
            transform.position += Vector3.right * dragonSpeed * Time.deltaTime;
        }
        else if (dragonSpeed >= 0)
        {
            dragonSpeed -= Time.deltaTime * 5;
            transform.position += Vector3.right * dragonSpeed * Time.deltaTime;
        }
    }

    private void FixedUpdate()
    {
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
                ChangePower(DragonPower.ICE_POWER);
                break;
            case "Eletric":
                ChangePower(DragonPower.ELETRIC_POWER);
                break;
            case "Fire":
                ChangePower(DragonPower.FIRE_POWER);
                break;
        }
    }

    void ChangePower(DragonPower newPower)
    {
        Debug.Log("Trocando power para: " + newPower);
        foreach (var clip in animator.runtimeAnimatorController.animationClips)
        {
            Debug.Log("Nome da animação no Animator: " + clip.name);
        }
        
        currentPower = newPower;

        switch (newPower)
        {
            case DragonPower.FIRE_POWER:
                spriteRenderer.sprite = fireDragonSprite;
                animatorOverride["idleStandard"] = idleFire;
                animatorOverride["flyStandard"] = flyFire;
                break;

            case DragonPower.ICE_POWER:
                spriteRenderer.sprite = iceDragonSprite;
                animatorOverride["idleStandard"] = idleIce;
                animatorOverride["flyStandard"] = flyIce;
                break;

            case DragonPower.ELETRIC_POWER:
                spriteRenderer.sprite = eletricDragonSprite;
                animatorOverride["idleStandard"] = idleEletric;
                animatorOverride["flyStandard"] = flyEletric;
                break;

            default: // STANDARD_POWER
                spriteRenderer.sprite = standardDragonSprite;
                animatorOverride["idle"] = idleStandard;
                animatorOverride["fly"] = flyStandard;
                break;
        }
    }
}
