using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(Animator))]
public class EnemyPatroller : MonoBehaviour
{
    [SerializeField] private float _moveSpeed = 2f;
    [SerializeField] private float _restTime = 2f;
    [SerializeField] private float _offset = 5f;

    private float _restTimer;
    private Vector2 _startPosition;
    private Vector2 _leftPosition;
    private Vector2 _rightPosition;
    private Vector2 _currentTarget;
    private bool _isResting;
    private Rigidbody2D _rigidbody2D;
    private Animator _animator;

    private void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
    }

    private void Start()
    {
        _startPosition = transform.position;
        _leftPosition = new Vector2(_startPosition.x - _offset, _startPosition.y);
        _rightPosition = new Vector2(_startPosition.x + _offset, _startPosition.y);

        _currentTarget = _leftPosition;
    }

    private void Update()
    {
        if (_isResting)
            Rest();
        else
            Patrol();
    }

    private void Patrol()
    {
        _rigidbody2D.position = Vector2.MoveTowards(_rigidbody2D.position, _currentTarget, _moveSpeed * Time.deltaTime);

        _animator.SetFloat(AnimatorController.Params.Speed, _moveSpeed);

        if (_rigidbody2D.position.x == _currentTarget.x && !_isResting)
        {
            Flip();
            _isResting = true;
            _restTimer = _restTime;
        }
    }

    private void Rest()
    {
        _animator.SetFloat(AnimatorController.Params.Speed, 0f);

        if (_restTimer > 0)
        {
            _restTimer -= Time.deltaTime;
        }
        else
        {
            _isResting = false;
            _currentTarget = _currentTarget == _rightPosition ? _leftPosition : _rightPosition;
            _restTimer = _restTime;
        }
    }

    private void Flip()
    {
        var localScale = transform.localScale;
        localScale.x *= -1f;
        transform.localScale = localScale;
    }
}