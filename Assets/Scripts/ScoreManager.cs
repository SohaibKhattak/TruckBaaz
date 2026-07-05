using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager instance;
    public int score = 0;
    private int highScore = 0;
    private float timer;

    void Awake()
{
    instance = this;
    score = 0;
    timer = 0f;
    highScore = PlayerPrefs.GetInt("HighScore", 0);
}

    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= 1f)
        {
            score++;
            timer = 0f;

            // Check if current score beats high score
            if (score > highScore)
            {
                highScore = score;
                PlayerPrefs.SetInt("HighScore", highScore);
            }

            UpdateUI();
        }
    }

    void UpdateUI()
    {
        if (UIManager.instance != null)
        {
            UIManager.instance.UpdateScore(score);
            UIManager.instance.UpdateHighScore(highScore);
        }
    }

    public int GetScore()
    {
        return score;
    }

    public int GetHighScore()
    {
        return highScore;
    }

    public void AddScore(int points)
    {
        score += points;

        if (score > highScore)
        {
            highScore = score;
            PlayerPrefs.SetInt("HighScore", highScore);
        }

        UpdateUI();
    }
}