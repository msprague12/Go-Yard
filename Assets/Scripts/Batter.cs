using UnityEngine;
using UnityEngine.InputSystem;

public class Batter : MonoBehaviour
{
    // Tracks whether the ball is currently inside the strike zone
    private bool ballInZone = false;

    // Reference to the ball that entered the zone
    private GameObject ballInHitZone;

    // Tracks how long the ball has been in the zone
    private float timeInZone = 0f;

    // The window (in seconds) for a perfect hit
    public float perfectWindow = 0.2f;

    // Power ranges
    public float minHorizontalPower = 5f;
    public float maxHorizontalPower = 15f;
    public float minVerticalPower = 3f;
    public float maxVerticalPower = 12f;

    void Update()
    {
        // Only allow swinging if the game is still active
        if (!Score.Instance.gameActive) return;

        // Track how long the ball has been in the zone
        if (ballInZone)
            timeInZone += Time.deltaTime;

        // Check if player pressed space and the ball is in the strike zone
        if (Keyboard.current.spaceKey.wasPressedThisFrame)
        {
            if (ballInZone && ballInHitZone != null)
            {
                // t = 0 is the moment ball entered zone (early)
                // t = perfectWindow is the sweet spot (perfect)
                // t > perfectWindow is late
                float t = Mathf.Clamp01(timeInZone / perfectWindow);

                // Arc peaks at perfect timing, falls off if too late
                float quality = 1f - Mathf.Abs(t - 1f);
                quality = Mathf.Clamp01(quality);

                float horizontal = Mathf.Lerp(minHorizontalPower, maxHorizontalPower, quality);
                float vertical = Mathf.Lerp(minVerticalPower, maxVerticalPower, quality);

                Baseball ball = ballInHitZone.GetComponent<Baseball>();
                if (ball != null)
                    ball.Hit(horizontal, vertical);

                Score.Instance.RegisterHit();
            }
            else
            {
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
            timeInZone = 0f; // Reset timer when ball enters
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
            timeInZone = 0f;
        }
    }
}