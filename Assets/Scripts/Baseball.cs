using UnityEngine;

public class Baseball : MonoBehaviour
{
    // Speed of ball across screen
    public float speed = 5f;

    private bool wasHit = false;
    private Rigidbody2D rb;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        // Disable gravity until the ball is hit
        rb.gravityScale = 0f;
    }

    void Update()
    {
        if (wasHit) return;

        transform.Translate(Vector2.left * speed * Time.deltaTime);

        if (transform.position.x < -12f)
        {
            Destroy(gameObject);
        }
    }

    // Called by Batter when the player swings
    public void Hit(float horizontalPower, float verticalPower)
    {
        wasHit = true;

        // Enable gravity so the ball arcs naturally
        rb.gravityScale = 1f;

        // Apply the hit force
        rb.linearVelocity = Vector2.zero;
        rb.AddForce(new Vector2(horizontalPower, verticalPower), ForceMode2D.Impulse);

        // Destroy after 4 seconds
        Destroy(gameObject, 4f);
    }
}