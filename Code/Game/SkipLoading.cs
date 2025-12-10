using UnityEngine;
using UnityEngine.SceneManagement;

public class SkipLoadingScreen : MonoBehaviour
{
    public string mainMenuScene = "MainMenu"; 

    void Update()
    {
        if (Input.anyKeyDown)
        {
            SceneManager.LoadScene(mainMenuScene);
        }
    }
}
