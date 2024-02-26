using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(AnimatorController))]
public class EnemyPatroller : MonoBehaviour
{
    [SerializeField] private float _moveSpeed = 2f;
    [SerializeField] private float _offset = 5f;
    [SerializeField] private float _restTime = 2f;

    private Quaternion TurnLeft = Quaternion.Euler(0f, 180f, 0f);
    private Quaternion TurnRight = Quaternion.identity;
    private float _tempRestTime;
    private bool _isResting = true;
    private Vector2 _startPosition;
    private Vector2 _currentTarget;
    private Rigidbody2D _rigidbody2D;
    private AnimatorController _knightAnimator;

    private void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _knightAnimator = GetComponent<AnimatorController>();
        _tempRestTime = _restTime;
        _startPosition = _rigidbody2D.position;
        ChangeTarget();
    }

    private void FixedUpdate()
    {
        if (_isResting)
            Rest();
        else
            Patrol();
    }

    private void Rest()
    {
        _restTime -= Time.deltaTime;

        if (_restTime < 0f)
        {
            _restTime = _tempRestTime;
            _isResting = false;
            _knightAnimator.SetWalking(true);
        }
    }

    private void ChangeTarget()
    {
        Vector2 leftPosition = new Vector2(_startPosition.x - _offset, _startPosition.y);
        Vector2 rightPosition = new Vector2(_startPosition.x + _offset, _startPosition.y);

        if (_rigidbody2D.position == leftPosition)
            _currentTarget = rightPosition;
        else
            _currentTarget = leftPosition;
    }

    private void Patrol()
    {
        if (_rigidbody2D.position == _currentTarget)
        {
            ChangeTarget();
            Flip();
            _knightAnimator.SetWalking(false);
            _isResting = true;
        }
        else
        {
            _rigidbody2D.position = Vector2.MoveTowards(_rigidbody2D.position, _currentTarget, _moveSpeed * Time.deltaTime);
        }
    }

    private void Flip()
    {
        switch (_currentTarget.x - _rigidbody2D.position.x)
        {
            case > 0:
                transform.localRotation = TurnLeft;
                break;
            case < 0:
                transform.localRotation = TurnRight;
                break;
        }
    }
}