using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Mover : MonoBehaviour
{
    [SerializeField] private float _moveSpeed;
    [SerializeField] private float _jumpForce;

    private Quaternion TurnLeft = Quaternion.Euler(0f, 180f, 0f);
    private Quaternion TurnRight = Quaternion.identity;
    private Rigidbody2D _rigidBody;

    public bool IsGrounded { get; private set; }

    private void Awake()
    {
        _rigidBody = GetComponent<Rigidbody2D>();
    }

    private void OnCollisionEnter2D(Collision2D other) => UpdateIsGrounded(other, true);

    private void OnCollisionExit2D(Collision2D other) => UpdateIsGrounded(other, false);

    public void Move(float direction)
    {
        Flip(direction);
        _rigidBody.velocity = new Vector2(direction * _moveSpeed, _rigidBody.velocity.y);
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

    private void Flip(float direction)
    {
        if (direction < 0)
            transform.localRotation = TurnLeft;
        else
            transform.localRotation = TurnRight;
    }
}
