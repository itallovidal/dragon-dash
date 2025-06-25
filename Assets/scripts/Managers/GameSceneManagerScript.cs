using System.Collections.Generic;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameSceneManagerScript : MonoBehaviour
{
    private AudioManagerScript audioManager;
    public Sprite[] btnMuteSprites;
    public Button buttonMute;

    public GameObject levelDescription;
    public GameObject[] level;
    private Dictionary<string, string> descriptionHashmap = new Dictionary<string, string>
    {
        { "clouds_level", "Batalhe entre as nuvens contra inimigos desafiadores em 'Altas Nuvens'" },
        { "florest_level", "Corra pela floresta e enfrente os desafios da selva em 'Savana Selvagem'" },
        { "space_level", "Desafie a gravidade e sobreviva ao caos do universo em 'Entre Estrelas'" }
    };

    private string chosenLevel = "clouds_level";

    public LevelLoader levelLoader;

    private void Awake()
    {
        // Procura o AudioManager na cena atual
        audioManager = FindFirstObjectByType<AudioManagerScript>();
        
        if (audioManager == null)
        {
            Debug.LogWarning("AudioManager não encontrado na cena!");
        }
        else if (btnMuteSprites != null)
        {
            // Inicializa o estado visual do botão baseado no volume atual
            InitializeMuteButtonState();
        }
    }

    public void StartLevel()
    {
        if (chosenLevel == null)
        {
            return;
        }

        // Debug.Log("Starting Level: " + chosenLevel);
        levelLoader.Transition(chosenLevel);
    }

    public void LoadScene(string sceneName)
    {
        if (sceneName == null)
        {
            Debug.Log("Ops! Esqueceu de passar o nome da cena");
            return;
        }

        Time.timeScale = 1f;
        levelLoader.Transition(sceneName);
    }

    public void ChangeLevel(GameObject elClicked)
    {
        // Debug.Log("Changing Description");

        Debug.Log(elClicked.gameObject.tag);

        if (levelDescription == null)
        {
            Debug.Log("Level Description is null");
            return;
        }

        levelDescription.gameObject.GetComponent<TextMeshProUGUI>().text = descriptionHashmap[elClicked.gameObject.tag];

        Animator level_animator = elClicked.gameObject.GetComponent<Animator>();

        ResetAnimators();
        level_animator.SetBool("chose_level", true);

        chosenLevel = elClicked.gameObject.tag;
    }

    private void InitializeMuteButtonState()
    {
        if (audioManager != null && audioManager.audioSource != null && buttonMute != null && btnMuteSprites != null && btnMuteSprites.Length >= 2)
        {
            if (PlayerPrefs.GetString("WantMusic", "true") == "true" && audioManager.audioSource.volume > 0)
            {
                buttonMute.GetComponent<UnityEngine.UI.Image>().sprite = btnMuteSprites[1]; // Som ligado
            }
            else if (PlayerPrefs.GetString("WantMusic", "true") == "false" || audioManager.audioSource.volume <= 0)
            {
                buttonMute.GetComponent<UnityEngine.UI.Image>().sprite = btnMuteSprites[0]; // Som mutado
            }
        }
    }

    public void MuteGame()
    {
        if (audioManager != null && audioManager.audioSource != null)
        {
            string currentWantMusic = PlayerPrefs.GetString("WantMusic", "true");
            
            if (currentWantMusic == "true")
            {
                // Muta o áudio
                audioManager.audioSource.volume = 0;
                audioManager.audioSource.Stop();
                buttonMute.GetComponent<UnityEngine.UI.Image>().sprite = btnMuteSprites[0];
                PlayerPrefs.SetString("WantMusic", "false");
                Debug.Log("Áudio mutado com sucesso!");
            }
            else
            {
                // Ativa o áudio com volume apropriado para a cena
                float volume = SceneManager.GetActiveScene().name == "MenuScene" ? 1f : 0.2f;
                audioManager.audioSource.volume = volume;
                audioManager.audioSource.Play();
                buttonMute.GetComponent<UnityEngine.UI.Image>().sprite = btnMuteSprites[1];
                PlayerPrefs.SetString("WantMusic", "true");
                Debug.Log("Áudio ativado com sucesso!");
            }
            
            // Salva as preferências
            PlayerPrefs.Save();
        }
        else
        {
            Debug.LogWarning("AudioManager ou AudioSource não encontrado!");
        }
    }

    public void ExitGame()
    {
        Debug.Log("ExitGame");
        Application.Quit();
    }

    private void ResetAnimators()
    {
        foreach (GameObject element in level)
        {
            Animator level_animator = element.gameObject.GetComponent<Animator>();
            level_animator.SetBool("chose_level", false);
        }
    }
}
