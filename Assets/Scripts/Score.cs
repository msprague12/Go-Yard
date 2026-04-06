using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

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

    // Reference to the game over panel
    public GameObject gameOverPanel;

    // Reference to the final score text
    public TMPro.TMP_Text finalScoreText;

    void Awake()
    {
        Instance = this;

        // Hide the game over panel at the start
        gameOverPanel.SetActive(false);
    }

    // Called by Batter script when player gets a hit
    public void RegisterHit(float quality)
    {
        if (!gameActive) return;

        score += 100;
        pitchCount++;
        Debug.Log("HIT! Score: " + score);

        FindObjectOfType<UI>().AnimateHit(quality);

        CheckGameOver();
    }

    // Called by Batter script when player misses
    public void RegisterMiss()
    {
        if (!gameActive) return;

        strikes++;
        pitchCount++;
        Debug.Log("MISS! Strikes: " + strikes);

        FindObjectOfType<UI>().AnimateMiss();

        CheckGameOver();
    }

    // Check if the game should end
    void CheckGameOver()
    {
        if (strikes >= maxStrikes || pitchCount >= maxPitches)
        {
            gameActive = false;
             // Show the game over panel with final score
            finalScoreText.text = "Final Score: " + score;
            gameOverPanel.SetActive(true);
            Debug.Log("GAME OVER! Final Score: " + score);
        }
    }

    // Called by the Play Again button to restart the game
    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}