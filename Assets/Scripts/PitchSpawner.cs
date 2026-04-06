using UnityEngine;

public class PitchSpawner : MonoBehaviour
{
    // The ball object to spawn 
    public GameObject ballPrefab;

    // How many seconds to wait between each pitch
    public float timeBetweenPitches = 2f;

    // Keeps track of how much time has passed
    private float timer;

    void Update()
    {
        // Don't throw any pitches until the game starts
        if (!Score.Instance.gameActive) return;

        // Add the time since last frame to our timer
        timer += Time.deltaTime;

        // When enough time has passed, throw a new pitch
        if (timer >= timeBetweenPitches)
        {
            SpawnBall();
            timer = 0f; // Reset the timer
        }
    }

    void SpawnBall()
    {
        // Create a new ball at the pitcher's position with no rotation
        Instantiate(ballPrefab, transform.position, Quaternion.identity);
    }
}