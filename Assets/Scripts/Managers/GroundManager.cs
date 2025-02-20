using UnityEngine;
using UnityEngine.Rendering.Universal;

public class GroundManager : MonoBehaviour
{

    [SerializeField] private float _movementSpeed;

    [SerializeField] private float _spawnTime;

    [SerializeField] private float _ySpawn;
    [SerializeField] private float _maxGround;

    [SerializeField] private GameObject _ground;
    private GameObject _lastSpawn = null;

    private float _time = 0;

    private Vector2 _spriteSize;
    private Vector2 _gameSize;

    private void HandleSpawn(float? x = null)
    {
        x ??= _gameSize.x;

        GameObject ground = Instantiate(_ground, new Vector2(x.Value, _ySpawn), Quaternion.identity, transform);
        if (_lastSpawn != null)
        {
            ground.transform.position = new Vector2(
                _lastSpawn.transform.position.x + _spriteSize.x,
                _lastSpawn.transform.position.y
            );
        }

        _lastSpawn = ground;
    }

    private void Start()
    {
        PixelPerfectCamera pixelCam = Camera.main.GetComponent<PixelPerfectCamera>();
        _gameSize = new Vector2(
            pixelCam.refResolutionX / (float)pixelCam.assetsPPU, pixelCam.refResolutionY / (float)pixelCam.assetsPPU
        );

        _spriteSize = _ground.GetComponentInChildren<SpriteRenderer>().size;

        // Original two ground objects
        // We set the first one to spawn on x = 0
        // Because otherwise they'll appear cropped untill timer does its thing.
        HandleSpawn(0);
        HandleSpawn();
    }

    private void Update()
    {
        _time += Time.deltaTime;
        if (_time >= _spawnTime)
        {
            if (transform.childCount >= _maxGround) return;
            HandleSpawn();

            _time = 0;
        }
    }

    private void FixedUpdate()
    {
        foreach (Transform child in transform)
        {
            Vector2 position = child.position;

            position.x -= _movementSpeed * Time.fixedDeltaTime;
            if (position.x < -_spriteSize.x)
                Destroy(child.gameObject);

            child.position = position;
        }
    }
}