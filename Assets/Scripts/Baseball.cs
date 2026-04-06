using UnityEngine;

public class Baseball : MonoBehaviour
{
    public float speed = 5f;

    void Update()
    {
        transform.Translate(Vector2.left * speed * Time.deltaTime);

        if (transform.position.x < -12f)
        {
            Destroy(gameObject);
        }
    }
}