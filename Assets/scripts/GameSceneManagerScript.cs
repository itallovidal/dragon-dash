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
        { "cloud_level", "Batalhe entre as nuvens contra inimigos desafiadores em 'Altas Nuvens'" },
        { "florest_level", "Corra pela floresta e enfrente os desafios da selva em 'Savana Selvagem'" },
        { "space_level", "Desafie a gravidade e sobreviva ao caos do universo em 'Entre Estrelas'" }
    };

    private string chosenLevel = "cloud_level";

    private bool isMovingUp = true;
    private float floatDistance = 0.5f;
    private float topLimit;
    private float bottomLimit;

    private void Start()
    {
        float startY = level[0].transform.position.y;
        topLimit = startY + floatDistance / 2f;
        bottomLimit = startY - floatDistance / 2f;
    }

    private void Awake()
    {
        audioManager = AudioManagerScript.instance;
        if (audioManager == null)
        {
            Debug.LogWarning("AudioManager não encontrado na cena!");
        }
    }

    private void Update()
    {
        MoveLevel();
    }

    public void MoveLevel()
    {
        if (level == null)
        {
            Debug.Log("Level is null");
            return;
        }

        for (int i = 0; i < level.Length; i++)
        {
            if (level[i] == null)
            {
                Debug.Log("Level " + i + " is null");
                return;
            }

            if (level[i].gameObject.tag != chosenLevel)
            {
                continue;
            }

            float direction = isMovingUp ? 1f : -1f;
            Vector3 move = new Vector3(0f, direction * Time.deltaTime * .25f, 0f);
            level[i].transform.position += move;

            if (level[i].transform.position.y >= topLimit)
            {
                isMovingUp = false;
            }
            else if (level[i].transform.position.y <= bottomLimit)
            {
                isMovingUp = true;
            }
        }
    }

    public void StartLevel()
    {
        if (chosenLevel == null)
        {
            return;
        }

        Debug.Log("Starting Level: " + chosenLevel);
        SceneManager.LoadScene(chosenLevel);
    }

    public void LoadScene(SceneAsset sceneAsset)
    {
        if (sceneAsset == null)
        {
            Debug.Log("Ops! Esqueceu de passar a cena");
            return;
        }
        SceneManager.LoadScene(sceneAsset.name);
    }

    public void ChangeLevelDescription(GameObject elClicked)
    {
        Debug.Log("Changing Description");

        Debug.Log(elClicked.gameObject.tag);

        if (levelDescription == null)
        {
            Debug.Log("Level Description is null");
            return;
        }

        levelDescription.gameObject.GetComponent<TextMeshProUGUI>().text = descriptionHashmap[elClicked.gameObject.tag];
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
                    Debug.Log("Áudio ativado com sucesso!");
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
}
