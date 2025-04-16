using UnityEngine;
using UnityEngine.SceneManagement;

public class GameSceneManagerScript : MonoBehaviour
{

    public string MenuPlaySceneName;

    public void MenuPlayGame() {
        SceneManager.LoadScene(MenuPlaySceneName);
    }

    public void ExitGame() {
        Debug.Log("ExitGame");
        Application.Quit();
    }
}
