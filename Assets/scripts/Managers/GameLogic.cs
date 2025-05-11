using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using System;

public class GameLogic : MonoBehaviour
{
    private int playerScore;
    public GameObject scoreText;

    private int highScore;
    public GameObject highScoreText;

    public GameObject gameOverlay;
    public GameObject overlayText;

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
        string text = overlayText.gameObject.GetComponent<TextMeshProUGUI>().text;
        
        if (text == "GameOver") {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

        if (text == "Pause") {
            gameOverlay.SetActive(false);
        }
        
        Time.timeScale = 1f;
    }

    public void GameOverlay(string text)
    {
        overlayText.gameObject.GetComponent<TextMeshProUGUI>().text = text;
        gameOverlay.SetActive(true);
        Time.timeScale = 0f;
    }
}
