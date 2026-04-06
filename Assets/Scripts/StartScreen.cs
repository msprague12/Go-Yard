using UnityEngine;

public class StartScreen : MonoBehaviour
{
    public GameObject startScreenPanel;

    void Start()
    {
        // Show the start screen when the game launches
        startScreenPanel.SetActive(true);
        // Keep the game paused until the player hits Play
        Score.Instance.gameActive = false;
    }

    public void OnPlayPressed()
    {
        // Hide the start screen
        startScreenPanel.SetActive(false);
        // Start game
        Score.Instance.gameActive = true;
    }
}