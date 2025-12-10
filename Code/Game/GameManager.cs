using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public GameObject gameOverPanel;

    void Awake()
    {
        if (instance == null)
            instance = this;
    }

    public void GameOver()
    {
        gameOverPanel.SetActive(true);

        // stop bgm
        if (BackgroundMusic.instance != null)
            BackgroundMusic.instance.StopMusic();

        // stop scrolling
        foreach (var s in Object.FindObjectsByType<InfiniteScroll>(FindObjectsSortMode.None))
            s.enabled = false;

        // stop enemies
        foreach (var e in Object.FindObjectsByType<EnemyMovement1>(FindObjectsSortMode.None)) e.enabled = false;
        foreach (var e in Object.FindObjectsByType<EnemyMovement2>(FindObjectsSortMode.None)) e.enabled = false;
        foreach (var e in Object.FindObjectsByType<EnemyMovement3>(FindObjectsSortMode.None)) e.enabled = false;

        // stop spawner
        var spawner = Object.FindFirstObjectByType<EnemySpawner>();
        if (spawner != null)
            spawner.enabled = false;
    }
}
