using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void PlayCutScene()
    {
        SceneManager.LoadScene("CutScene"); 
    }

    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("Player Quit the Game");
    }
}
