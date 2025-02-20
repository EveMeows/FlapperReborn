using UnityEngine;

public class Pipes : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private float _killBoundary;

    void FixedUpdate()
    {
        Vector2 position = transform.position;

        position.x -= _speed * Time.fixedDeltaTime;
        if (position.x < _killBoundary)
            Destroy(gameObject);

        transform.position = position;
    }
}
