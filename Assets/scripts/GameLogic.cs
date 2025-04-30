using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class GameLogic : MonoBehaviour
{
    private int playerScore;
    public GameObject scoreText;

    public GameObject gameOverScreen;

    [ContextMenu("Add 1 point to score")]
    public void AddScore()
    {
        playerScore++;
        scoreText.gameObject.GetComponent<TextMeshProUGUI>().text = playerScore.ToString();
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
