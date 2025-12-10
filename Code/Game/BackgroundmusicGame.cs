using UnityEngine;
using UnityEngine.SceneManagement;

public class BackgroundMusic : MonoBehaviour
{
    public static BackgroundMusic instance;
    public AudioSource bgmSource;

    void Awake()
    {
        if (instance == null)
            instance = this;
        else
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);

        if (bgmSource == null)
            bgmSource = GetComponent<AudioSource>();
    }

    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "MainMenu")
            StopMusic();     
        else
            PlayMusic();     
    }

    public void PlayMusic()
    {
        if (!bgmSource.isPlaying)
            bgmSource.Play();
    }

    public void StopMusic()
    {
        if (bgmSource.isPlaying)
            bgmSource.Stop();
    }
}
