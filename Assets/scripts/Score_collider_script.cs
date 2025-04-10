using UnityEngine;

public class PipeCenterColliderScript : MonoBehaviour
{
    Game_logic game_logic;

    void Start()
    {
        game_logic = GameObject.FindGameObjectWithTag("Logic").GetComponent<Game_logic>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.layer == 3)
        {
            game_logic.AddScore();
        }
    }
}
