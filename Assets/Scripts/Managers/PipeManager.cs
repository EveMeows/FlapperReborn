using UnityEngine;

public class PipeManager : MonoBehaviour
{
    [SerializeField] private GameObject _pipes;

    [SerializeField] private float _spawnTime;

    /// <summary>
    /// X is Max spawn Y, while Y is Min spawn Y
    /// </summary>
    [SerializeField] private Vector2 _spawnBoundary;

    private float _time = 0;

    void Update()
    {
        _time += Time.deltaTime;
        if (_time >= _spawnTime)
        {
            GameObject pipes = Instantiate(_pipes, transform.position, Quaternion.identity, transform);
            pipes.transform.position = new Vector2(
                transform.position.x,
                Random.Range(_spawnBoundary.y, _spawnBoundary.x)
            );

            _time = 0;
        }
    }
}
