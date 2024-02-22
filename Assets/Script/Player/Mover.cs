using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Mover : MonoBehaviour
{
    public bool _isGrounded;
    private Rigidbody2D _rigidBody;
    private SpriteRenderer _spriteRenderer;

    private void Awake()
    {
        _rigidBody = GetComponent<Rigidbody2D>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void OnCollisionEnter2D(Collision2D other) => IsGroundedUpdate(other, true);

    private void OnCollisionExit2D(Collision2D other) => IsGroundedUpdate(other, false);

    public void Move(float direction)
    {
        _rigidBody.velocity = new Vector2(direction, _rigidBody.velocity.y);

        if (direction < 0)
            _spriteRenderer.flipX = true;
        else
            _spriteRenderer.flipX = false;
    }

    public void Jump(float jumpForce)
    {
        if (_isGrounded)
            _rigidBody.velocity = new Vector2(_rigidBody.velocity.x, jumpForce);
    }

    private void IsGroundedUpdate(Collision2D other, bool value)
    {
        if (other.gameObject.GetComponent<Ground>() != null)
            _isGrounded = value;
    }
}
