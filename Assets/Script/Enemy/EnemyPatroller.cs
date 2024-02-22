using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(KnightAnimator))]
public class EnemyPatroller : MonoBehaviour
{
    [SerializeField] private float _moveSpeed = 2f;
    [SerializeField] private float _offset = 5f;
    [SerializeField] private float _restTime = 2f;

    private float _tempRestTime;
    private bool _isResting = true;
    private Vector2 _startPosition;
    private Vector2 _currentTarget;
    private Rigidbody2D _rigidbody2D;
    private KnightAnimator _knightAnimator;

    private void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _knightAnimator = GetComponent<KnightAnimator>();
    }

    private void Start()
    {
        _tempRestTime = _restTime;
        _startPosition = _rigidbody2D.position;
        UpdateTarget();
    }

    private void Update()
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

    private void UpdateTarget()
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
            UpdateTarget();
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
        var localScale = transform.localScale;
        localScale.x *= -1f;
        transform.localScale = localScale;
    }
}