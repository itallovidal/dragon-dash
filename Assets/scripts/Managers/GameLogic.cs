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

    private PlayerScript playerScript;
    private AudioManagerScript audioManager;

    private string activeScene;

    private void Awake()
    {
        // Se não encontrar, busca na cena
        if (audioManager == null)
        {
            audioManager = FindFirstObjectByType<AudioManagerScript>();
        }

        // Se não tiver enccontrado sinaliza
        if (audioManager == null)
        {
            Debug.LogWarning("AudioManager não encontrado na cena!");
        }
    }
 
    void Start()
    {
        activeScene = SceneManager.GetActiveScene().name;
        StartHighScore();
        playerScript = FindFirstObjectByType<PlayerScript>();
    }

    private void StartHighScore()
    {
        switch (activeScene)
        {
            case "clouds_level":
                highScore = PlayerPrefs.GetInt("Clouds_highscore", 0);
                break;
            case "florest_level":
                highScore = PlayerPrefs.GetInt("Florest_highscore", 0);
                break;
            case "space_level":
                highScore = PlayerPrefs.GetInt("Space_highscore", 0);
                break;
            default:
                Debug.Log("Cena não reconhecida para o highscore: " + activeScene);
                break;
        }
        highScoreText.gameObject.GetComponent<TextMeshProUGUI>().text = "HIGHSCORE: " + highScore.ToString();
    }

    [ContextMenu("Add 1 point to score")]
    public void AddScore()
    {
        playerScore++;
        scoreText.gameObject.GetComponent<TextMeshProUGUI>().text = playerScore.ToString();

        if (highScore < playerScore) {
            UpdateHighScore();
        }
    }

    private void UpdateHighScore()
    {
        switch (activeScene)
        {
            case "clouds_level":
                PlayerPrefs.SetInt("Clouds_highscore", playerScore);
                break;
            case "florest_level":
                PlayerPrefs.SetInt("Florest_highscore", playerScore);
                break;
            case "space_level":
                PlayerPrefs.SetInt("Space_highscore", playerScore);
                break;
            default:
                Debug.LogWarning("Cena não reconhecida para atualizar o highscore: " + activeScene);
                break;
        }
    }
    public void RestartGame()
    {
        string text = overlayText.gameObject.GetComponent<TextMeshProUGUI>().text;

        if (text == "GameOver")
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

        if (text == "Pause")
        {
            gameOverlay.SetActive(false);
            playerScript.isGameOverlay = false;
        }

        Time.timeScale = 1f;

        if (audioManager != null)
        {
            audioManager.audioSource.volume = 0.2f;
        }
    }

    public void GameOverlay(string text)
    {
        overlayText.gameObject.GetComponent<TextMeshProUGUI>().text = text;
        gameOverlay.SetActive(true);
        playerScript.isGameOverlay = true;
        Time.timeScale = 0f;
        
        if (audioManager != null)
        {
            audioManager.audioSource.volume = 0;
        }
    }
}
