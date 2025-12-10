using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager instance;

    public int score = 0;
    public int highScore = 0;

    public TextMeshProUGUI[] scoreTexts;
    public TextMeshProUGUI[] highScoreTexts;

    void Awake()
    {
        if (instance == null) instance = this;

        highScore = PlayerPrefs.GetInt("HighScore", 0);
    }

    void Start()
    {
        UpdateUI();
    }

    public void AddScore(int amount)
    {
        score += amount;

        if (score > highScore)
        {
            highScore = score;
            PlayerPrefs.SetInt("HighScore", highScore);
        }

        UpdateUI();
    }

    void UpdateUI()
    {
        foreach (var t in scoreTexts)
            if (t != null) t.text = "Score: " + score;

        foreach (var t in highScoreTexts)
            if (t != null) t.text = "High Score: " + highScore;
    }
}
