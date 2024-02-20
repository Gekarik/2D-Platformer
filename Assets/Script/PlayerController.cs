using UnityEngine;

[RequireComponent(typeof(Rigidbody2D),typeof(Animator))]
public class PlayerController : MonoBehaviour
{
    private const string Horizontal = nameof(Horizontal);
    private const string Vertical = nameof(Vertical);

    [SerializeField] private float _movementSpeed;
    [SerializeField] private float _jumpForce;
    [SerializeField] private float _radiusOfSensor;
    [SerializeField] private Transform _groundSensor;
    [SerializeField] private LayerMask _groundLayer;

    private bool isFacingRight = false;
    private float _moveInput;
    private Rigidbody2D _rigidBody;
    private Animator _animator;
    
    private void Awake()
    {
        _rigidBody = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
    }

    private void Update()
    {
        Move();
        Flip();
        Jump();
    }

    private void Move()
    {
        _moveInput = Input.GetAxis(Horizontal);
        _rigidBody.velocity = new Vector2(_moveInput * _movementSpeed, _rigidBody.velocity.y);

        _animator.SetFloat(AnimatorPlayerController.Params.Speed, Mathf.Abs(_moveInput * _movementSpeed));
    }

    private void Flip()
    {
        if (isFacingRight == true && _moveInput < 0f || isFacingRight == false && _moveInput > 0f)
        {
            isFacingRight = !isFacingRight;
            Vector3 localscale = transform.localScale;
            localscale.x *= -1f;
            transform.localScale = localscale;
        }
    }

    private bool IsGrounded() => Physics2D.OverlapCircle(_groundSensor.position, _radiusOfSensor, _groundLayer);

    private void Jump()
    {
        if (Input.GetButtonDown("Jump") && IsGrounded())
            _rigidBody.velocity = new Vector2(_rigidBody.velocity.x, _jumpForce);

        if (Input.GetButtonDown("Jump") && _rigidBody.velocity.y > 0f)
            _rigidBody.velocity = new Vector2(_rigidBody.velocity.x, _rigidBody.velocity.y * 0.5f);
    }
}
