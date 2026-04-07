using UnityEngine;

public class Audio : MonoBehaviour
{
    public static Audio Instance;

    [Header("Sound Effects")]
    // The crack of the bat on a successful hit
    public AudioClip batCrack;
    // The swoosh sound for a swing and miss
    public AudioClip swoosh;
    // Crowd roar for a perfect hit
    public AudioClip perfectHitCheer;
    // Crowd disappointment for a strike
    public AudioClip strikeSound;
    // Game over sound
    public AudioClip gameOverSound;

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

    // Called when the player gets a perfect hit
    public void PlayPerfectHit()
    {
        sfxSource.PlayOneShot(batCrack);
        sfxSource.PlayOneShot(perfectHitCheer);
    }

    // Call this when the player misses
    public void PlaySwoosh()
    {
        sfxSource.PlayOneShot(swoosh);
    }

    // Called when a strike is registered
    public void PlayStrike()
    {
        sfxSource.PlayOneShot(strikeSound);
    }

    // Called when the game ends
    public void PlayGameOver()
    {
        // Stop the background crowd noise
        backgroundSource.Stop();
        sfxSource.PlayOneShot(gameOverSound);
    }
}