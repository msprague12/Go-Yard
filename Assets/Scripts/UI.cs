using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    // References to the text elements on screen
    public TMP_Text scoreText;
    public TMP_Text strikesText;
    public TMP_Text pitchCounter;

    void Update()
    {
        // Update the text every frame to match the score values
        scoreText.text = "Score: " + Score.Instance.score;
        strikesText.text = "Strikes: " + Score.Instance.strikes;
        pitchCounter.text = "Pitches: " + Score.Instance.pitchCount;
    }
}