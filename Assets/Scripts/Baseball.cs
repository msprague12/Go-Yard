using UnityEngine;

public class Baseball : MonoBehaviour
{
    // Speed of ball across screen
    public float speed = 5f;

    void Update()
    {
        // Ball travels from right to left on screen 
        transform.Translate(Vector2.left * speed * Time.deltaTime);

        // Destroy ball if it moves off screen
        if (transform.position.x < -12f)
        {
            if (gameObject != null)
            {
                Destroy(gameObject);
            }
        }
    }
}