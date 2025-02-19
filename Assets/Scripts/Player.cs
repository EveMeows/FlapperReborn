using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    [SerializeField] private Rigidbody2D _body;

    [SerializeField] private float _jumpForce;

    public void OnJumpPressed(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            _body.linearVelocityY = _jumpForce;
        }
    }
}
