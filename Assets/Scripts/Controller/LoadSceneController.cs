using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadSceneController : MonoBehaviour
{
    public string sceneName = "";


    public void LoadScene()
    {
        SceneManager.LoadScene(this.sceneName);
    }
    public void LoadScene(string sceneName = "")
    {
        if (sceneName == "")
            SceneManager.LoadScene(this.sceneName);
        else
            SceneManager.LoadScene(sceneName);
    }
}
