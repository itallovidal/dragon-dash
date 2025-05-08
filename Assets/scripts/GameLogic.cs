using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class GameLogic : MonoBehaviour
{
    private int playerScore;
    public GameObject scoreText;

    private int highScore;
    public GameObject highScoreText;

    public GameObject gameOverScreen;

    void Start()
    {
        highScore = PlayerPrefs.GetInt("highscore", 0);
        highScoreText.gameObject.GetComponent<TextMeshProUGUI>().text = "HIGHSCORE: " + highScore.ToString();
    }

    [ContextMenu("Add 1 point to score")]
    public void AddScore()
    {
        playerScore++;
        scoreText.gameObject.GetComponent<TextMeshProUGUI>().text = playerScore.ToString();

        if (highScore < playerScore) {
            PlayerPrefs.SetInt("highscore", playerScore);
        }
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        Time.timeScale = 1f;
    }

    public void GameOver()
    {
        gameOverScreen.SetActive(true);
        Time.timeScale = 0f;
    }
}
