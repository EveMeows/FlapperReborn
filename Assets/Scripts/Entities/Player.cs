using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    #region Editor
    [SerializeField] private Rigidbody2D _body;
    [SerializeField] private Animator _animator;

    [SerializeField] private GameObject _flasher;

    [SerializeField] private GameObject _managers;
    [SerializeField] private GameObject _container;
    
    [SerializeField] private float _jumpForce;

    [SerializeField] private float _downwardAngle;
    [SerializeField] private float _upwardAngle;

    /// <summary>
    /// X is min, Y is max.
    /// </summary>
    [SerializeField] private Vector2 _rotationBoundaries;

    [SerializeField] private UnityEvent<int> _onScoreChanged;
    #endregion

    private float _rotationSpeed;
    private float _rotationAngle;
    private float _rotation;

    private int _score = 0;

    private bool _dead;

    private void Kill()
    { 
        _flasher.SetActive(true);

        // Hide score count
        _container.SetActive(false);

        // Is this a hack?
        // What better way is there to stop anims...
        // _animator.speed = 0;
        _animator.enabled = false;

        // Disable everything
        // My precious little hack....
        // (Do not do this)
        foreach (MonoBehaviour mb in _managers.GetComponentsInChildren<MonoBehaviour>())
            mb.enabled = false;

        _dead = true;

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        GameObject collider = collision.gameObject;
        if (collider.layer == 7) Kill();
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject collider = collision.gameObject;

        switch (collider.layer)
        {
            // Score
            case 3:
                _score++;
                _onScoreChanged?.Invoke(_score);

                break;

            // Pipe
            case 6:
                Kill();
                break;
            
            default:
                return;
        }


        // Disable so it cannot be triggered twice.
        collision.enabled = false;
    }


    public void Awake()
    {
        _rotationAngle = _downwardAngle;
    }

    public void Update()
    {
        // I have to make this check here
        // For some reason, it wouldn't work in FixedUpdate
        if (_body.linearVelocity.y < 0)
        {
            _rotationAngle = _downwardAngle;
            _rotationSpeed = _rotationBoundaries.x;
            
            _animator.speed = 1f;
        }
    }

    public void FixedUpdate()
    {
        // Bird rotation
        // Idk why unity does this
        _rotation = Mathf.MoveTowardsAngle(_rotation, _rotationAngle, _rotationSpeed);
        transform.rotation = Quaternion.Euler(0, 0, _rotation);
    }

    public void OnJumpPressed(InputAction.CallbackContext context)
    {
        if (_dead) return;

        // Apply jump force
        // Only when the button is first pressed.
        if (context.started)
        {    
            _body.linearVelocityY = _jumpForce;
            _rotationAngle = _upwardAngle;
            _rotationSpeed = _rotationBoundaries.y;

            _animator.speed = 3.5f;
        }
    }
}
