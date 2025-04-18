using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameSceneManagerScript : MonoBehaviour
{

    public void LoadScene(SceneAsset sceneAsset)
    {
        if (sceneAsset == null)
        {
            Debug.Log("Ops! Esqueceu de passar a cena");
            return;
        }
        SceneManager.LoadScene(sceneAsset.name);
    }

    public void ExitGame() {
        Debug.Log("ExitGame");
        Application.Quit();
    }
}
