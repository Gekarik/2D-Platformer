using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Mover : MonoBehaviour
{
    [SerializeField] private float _moveSpeed;
    [SerializeField] private float _jumpForce;

    private Rigidbody2D _rigidBody;
    private SpriteRenderer _spriteRenderer;

    public bool IsGrounded { get; private set; }

    private void Awake()
    {
        _rigidBody = GetComponent<Rigidbody2D>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void OnCollisionEnter2D(Collision2D other) => UpdateIsGrounded(other, true);

    private void OnCollisionExit2D(Collision2D other) => UpdateIsGrounded(other, false);

    public void Move(float direction)
    {
        _rigidBody.velocity = new Vector2(direction * _moveSpeed, _rigidBody.velocity.y);

        if (direction < 0)
            _spriteRenderer.flipX = true;
        else
            _spriteRenderer.flipX = false;
    }

    public void Jump()
    {
        if (IsGrounded)
            _rigidBody.velocity = new Vector2(_rigidBody.velocity.x, _jumpForce);
    }

    private void UpdateIsGrounded(Collision2D other, bool value)
    {
        other.gameObject.TryGetComponent(out Ground ground);

        if (ground != null)
            IsGrounded = value;
    }
}
