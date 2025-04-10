using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Game_logic : MonoBehaviour
{
    private int playerScore;
    public Text scoreText;

    public GameObject gameOverScreen;

    [ContextMenu("Add 1 point to score")]
    public void AddScore()
    {
        playerScore++;
        scoreText.text = playerScore.ToString();
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name) ;
    }

    public void GameOver()
    {
        gameOverScreen.SetActive(true);
    }
}
