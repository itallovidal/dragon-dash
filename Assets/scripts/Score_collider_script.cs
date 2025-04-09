using UnityEngine;

public class PipeCenterColliderScript : MonoBehaviour
{
    LogicManager logicManager;

    void Start()
    {
        logicManager = GameObject.FindGameObjectWithTag("Logic").GetComponent<LogicManager>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.layer == 3)
        {
            logicManager.AddScore();
        }
    }
}
