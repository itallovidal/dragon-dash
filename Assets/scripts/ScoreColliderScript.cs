using UnityEngine;

public class ScoreColliderScript : MonoBehaviour
{
    GameLogic gameLogic;

    void Start()
    {
        gameLogic = GameObject.FindGameObjectWithTag("Logic").GetComponent<GameLogic>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.layer == 3)
        {
            gameLogic.AddScore();
        }
    }
}
