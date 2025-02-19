using System;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    #region Editor
    [SerializeField] private Rigidbody2D _body;
    
    [SerializeField] private float _jumpForce;

    [SerializeField] private float _downwardAngle;
    [SerializeField] private float _upwardAngle;

    [SerializeField] private float _rotationSpeed;
    #endregion

    private float _rotationAngle;
    private float _rotation;

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
        // Apply jump force
        // Only when the button is first pressed.
        if (context.started)
        {    
            _body.linearVelocityY = _jumpForce;
            _rotationAngle = _upwardAngle;
        }
    }
}
