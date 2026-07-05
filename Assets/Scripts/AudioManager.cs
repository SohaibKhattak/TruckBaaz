using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    [Header("Audio Sources")]
    public AudioSource musicSource;
    public AudioSource sfxSource;

    [Header("Audio Clips")]
    public AudioClip backgroundMusic;
    public AudioClip collectSound;
    public AudioClip hitSound;
    public AudioClip gameOverSound;

    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        if (backgroundMusic != null)
        {
            musicSource.clip = backgroundMusic;
            musicSource.loop = true;
            musicSource.Play();
        }
    }

    public void PlayCollect()
    {
        if (collectSound != null)
            sfxSource.PlayOneShot(collectSound);
    }

    public void PlayHit()
    {
        if (hitSound != null)
            sfxSource.PlayOneShot(hitSound);
    }

    public void PlayGameOver()
    {
        if (gameOverSound != null)
            sfxSource.PlayOneShot(gameOverSound);
    }
}