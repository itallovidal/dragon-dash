using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManagerScript : MonoBehaviour
{

    public string sceneName;

    public void PlayGame() {
        SceneManager.LoadScene(sceneName);
    }

    public void ExitGame() {
        Debug.Log("ExitGame");
        Application.Quit();
    }
}
