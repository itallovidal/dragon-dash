using System.Collections.Generic;
using Microsoft.Unity.VisualStudio.Editor;
using TMPro;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
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


    private void Awake()
    {
        audioManager = AudioManagerScript.instance;
        if (audioManager == null)
        {
            Debug.LogWarning("AudioManager não encontrado na cena!");
        }

        // Sincroniza o sprite do botão com o volume atual
        if (buttonMute != null && audioManager.audioSource.volume == 0)
        {
            buttonMute.GetComponent<UnityEngine.UI.Image>().sprite = btnMuteSprites[0];
        }
    }

    public void StartLevel()
    {
        if (chosenLevel == null)
        {
            return;
        }

        // Debug.Log("Starting Level: " + chosenLevel);
        SceneManager.LoadScene(chosenLevel);
    }

    public void LoadScene(SceneAsset sceneAsset)
    {
        if (sceneAsset == null)
        {
            Debug.Log("Ops! Esqueceu de passar a cena");
            return;
        }

        Time.timeScale = 1f;
        SceneManager.LoadScene(sceneAsset.name);
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

    public void MuteGame()
    {
        if (audioManager != null && audioManager.audioSource != null)
        {
            switch (audioManager.audioSource.volume)
            {
                case 0:
                    audioManager.audioSource.volume = 1;
                    buttonMute.GetComponent<UnityEngine.UI.Image>().sprite = btnMuteSprites[1];
                    // Debug.Log("Áudio ativado com sucesso!");
                    break;
                case 1:
                    audioManager.audioSource.volume = 0;
                    buttonMute.GetComponent<UnityEngine.UI.Image>().sprite = btnMuteSprites[0];
                    Debug.Log("Áudio mutado com sucesso!");
                    break;
            }
        }

    }

    public void ExitGame()
    {
        Debug.Log("ExitGame");
        Application.Quit();
    }

    private void ResetAnimators() {
        foreach (GameObject element in level) {
            Animator level_animator = element.gameObject.GetComponent<Animator>();
            level_animator.SetBool("chose_level", false);
        }
    }
}
