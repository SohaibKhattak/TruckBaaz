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
        // Make AudioManager persist between scenes
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        // Play background music automatically
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