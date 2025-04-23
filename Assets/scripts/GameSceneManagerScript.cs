using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameSceneManagerScript : MonoBehaviour
{
    public GameObject levelDescription;
    private Dictionary<string, string> descriptionHashmap = new Dictionary<string, string>
    {
        { "cloud_level", "Batalhe entre as nuvens contra inimigos desafiadores em 'Altas Nuvens'" },
        { "forest_level", "Corra pela floresta e enfrente os desafios da selva em 'Savana Selvagem'" },
        { "space_level", "Desafie a gravidade e sobreviva ao caos do universo em 'Entre Estrelas'." }
    };

    private string chosenLevel = "cloud_level";


    public void StartLevel()
    {
        if(chosenLevel == null)
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

        if(levelDescription == null)
        {
            Debug.Log("Level Description is null");
            return;
        }

        levelDescription.gameObject.GetComponent<TextMeshProUGUI>().text = descriptionHashmap[elClicked.gameObject.tag];
        chosenLevel = elClicked.gameObject.tag;
    }

    public void ExitGame() {
        Debug.Log("ExitGame");
        Application.Quit();
    }
}
