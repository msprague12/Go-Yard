using UnityEngine;
using UnityEngine.InputSystem;

public class Batter : MonoBehaviour
{
    // Tracks whether the ball is currently inside the strike zone
    private bool ballInZone = false;

    // Reference to the ball that entered the zone
    private GameObject ballInHitZone;

    void Update()
    {
        // Only allow swinging if the game is still active
        if (!Score.Instance.gameActive) return;

        // Check if player pressed space and the ball is in the strike zone
        if (Keyboard.current.spaceKey.wasPressedThisFrame)
        {
            if (ballInZone && ballInHitZone != null)
            {
                // Successful swing
                Score.Instance.RegisterHit();
                Destroy(ballInHitZone);
            }
            else
            {
                // Missed swing
                Score.Instance.RegisterMiss();
            }
        }
    }

    // Called when something enters the hit zone trigger
    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Something entered the zone: " + other.gameObject.name);
        if (other.gameObject.name == "Baseball(Clone)")
        {
            ballInZone = true;
            ballInHitZone = other.gameObject;
            Debug.Log("Ball is in the zone!");

        }
    }

    // Called when something leaves the hit zone trigger
    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.name == "Baseball(Clone)")
        {
            ballInZone = false;
            ballInHitZone = null;
        }
    }
}