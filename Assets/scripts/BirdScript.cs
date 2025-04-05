using UnityEngine;

public class BirdScript : MonoBehaviour
{
    public float jumpStrength = 4f;
    public Rigidbody2D myRigidBody;
    float spawnSpeed = 6f;
    LogicManager logicManager;
    bool isAlive = true;

    void Start()
    {
        logicManager = GameObject.FindGameObjectWithTag("Logic").GetComponent<LogicManager>();
    }


    void Update()
    {
        spawn();
        if (Input.GetKeyDown(KeyCode.Space) && isAlive) {
            myRigidBody.linearVelocity = Vector2.up * jumpStrength;
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
                spawnSpeed-= Time.deltaTime * 5;
                transform.position += Vector3.right * spawnSpeed * Time.deltaTime;
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        logicManager.GameOver();
        isAlive = false;
    }
}
