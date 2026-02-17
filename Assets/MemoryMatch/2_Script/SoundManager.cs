using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;

    [Header("Audio Source")]
    public AudioSource audioSource;

    [Header("Sound Clips")]
    public AudioClip flipSound;
    public AudioClip matchSound;
    public AudioClip mismatchSound;
    public AudioClip gameOverSound;

    void Awake()
    {
        // Singleton Pattern
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Play Flip Sound
    public void PlayFlip()
    {
        PlaySound(flipSound);
    }

    // Play Match Sound
    public void PlayMatch()
    {
        PlaySound(matchSound);
    }

    // Play Mismatch Sound
    public void PlayMismatch()
    {
        PlaySound(mismatchSound);
    }

    // Play Game Over Sound
    public void PlayGameOver()
    {
        PlaySound(gameOverSound);
    }

    // Common Play Method
    void PlaySound(AudioClip clip)
    {
        if (clip != null && audioSource != null)
        {
            audioSource.PlayOneShot(clip);
        }
    }
}
