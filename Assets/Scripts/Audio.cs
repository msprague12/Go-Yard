using UnityEngine;

public class Audio : MonoBehaviour
{
    public static Audio Instance;

    [Header("Sound Effects")]
    // The crack of the bat on a successful hit
    public AudioClip batCrack;
    // The swoosh sound for a swing and miss
    public AudioClip swoosh;

    [Header("Background")]
    // Looping crowd noise played throughout the game
    public AudioClip crowdNoise;

    // Two separate audio sources — one for SFX, one for background
    private AudioSource sfxSource;
    private AudioSource backgroundSource;

    void Awake()
    {
        Instance = this;

        // Create an audio source for sound effects
        sfxSource = gameObject.AddComponent<AudioSource>();

        // Create a separate audio source for background noise
        backgroundSource = gameObject.AddComponent<AudioSource>();

        // Set up background audio to loop continuously
        backgroundSource.clip = crowdNoise;
        backgroundSource.loop = true;
        backgroundSource.volume = 0.3f; // Keep crowd noise subtle
        backgroundSource.Play();
    }

    // Call this when the player gets a hit
    public void PlayBatCrack()
    {
        sfxSource.PlayOneShot(batCrack);
    }

    // Call this when the player misses
    public void PlaySwoosh()
    {
        sfxSource.PlayOneShot(swoosh);
    }
}