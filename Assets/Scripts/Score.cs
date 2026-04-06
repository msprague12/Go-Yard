using UnityEngine;
using UnityEngine.InputSystem;

public class Score : MonoBehaviour
{

    public static Score Instance;

    // Game stats
    public int score = 0;
    public int strikes = 0;
    public int pitchCount = 0;

    // Game limits
    public int maxStrikes = 3;
    public int maxPitches = 10;

    // Is the game still going?
    public bool gameActive = true;

    void Awake()
    {
        Instance = this;
    }

    // Called by Batter script when player gets a hit
    public void RegisterHit()
    {
        if (!gameActive) return;

        score += 100;
        pitchCount++;
        Debug.Log("HIT! Score: " + score);

        CheckGameOver();
    }

    // Called by Batter script when player misses
    public void RegisterMiss()
    {
        if (!gameActive) return;

        strikes++;
        pitchCount++;
        Debug.Log("MISS! Strikes: " + strikes);

        CheckGameOver();
    }

    // Check if the game should end
    void CheckGameOver()
    {
        if (strikes >= maxStrikes || pitchCount >= maxPitches)
        {
            gameActive = false;
            Debug.Log("GAME OVER! Final Score: " + score);
        }
    }
}