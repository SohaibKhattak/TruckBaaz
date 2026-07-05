using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    public int lives = 3;
    private bool isInvincible = false;

    void Start()
{
    lives = 3;
    isInvincible = false;
    
    if (UIManager.instance != null)
    {
        UIManager.instance.UpdateLives(lives);
        UIManager.instance.UpdateScore(0);
        UIManager.instance.UpdateHighScore(
            PlayerPrefs.GetInt("HighScore", 0));
    }
}

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Obstacle"))
        {
            // Big screen shake
            if (CameraShake.instance != null)
                CameraShake.instance.Shake(0.5f, 0.3f);

            lives = 0;
            if (UIManager.instance != null)
                UIManager.instance.UpdateLives(lives);
            Debug.Log("Hit by Bus! Instant Death!");
            GameOver();
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Barrier"))
        {
            if (!isInvincible)
            {
                lives--;
                Debug.Log("Hit barrier! Lives remaining: " + lives);

                // Small screen shake
                if (CameraShake.instance != null)
                    CameraShake.instance.Shake(0.2f, 0.1f);

                if (UIManager.instance != null)
                    UIManager.instance.UpdateLives(lives);

                if (AudioManager.instance != null)
                    AudioManager.instance.PlayHit();

                if (lives <= 0)
                {
                    GameOver();
                }
                else
                {
                    StartCoroutine(InvincibilityFrames());
                }
            }
        }
    }

    System.Collections.IEnumerator InvincibilityFrames()
    {
        isInvincible = true;
        yield return new WaitForSeconds(1.5f);
        isInvincible = false;
    }

    void GameOver()
    {
        Debug.Log("GAME OVER!");

        if (AudioManager.instance != null)
            AudioManager.instance.PlayGameOver();

        if (ScoreManager.instance != null)
        {
            PlayerPrefs.SetInt("FinalScore",
                ScoreManager.instance.GetScore());
        }
        SceneManager.LoadScene("GameOver");
    }
}