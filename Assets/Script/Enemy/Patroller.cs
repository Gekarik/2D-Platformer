using UnityEngine;

[RequireComponent(typeof(Enemy))]
public class Patroller : MonoBehaviour
{
    public bool IsResting { get; private set; }

    [SerializeField] private float _restTime = 2f;
    [SerializeField] private float _offset = 5f;

    private float _tempRestTime;
    private Vector2 _startPosition;
    private Vector2 _currentTarget;
    private Enemy _enemy;

    private void Awake()
    {
        _enemy = GetComponent<Enemy>();
        _tempRestTime = _restTime;
    }

    public void SetStartPositions(Vector2 startPosition)
    {
        _startPosition = startPosition;
        _currentTarget = startPosition;
    }

    public void Rest()
    {
        _restTime -= Time.deltaTime;

        if (_restTime < 0f)
        {
            _restTime = _tempRestTime;
            IsResting = false;
        }
    }

    public Vector2 Patrol(Vector2 currentPosition)
    {
        _enemy.Flip(_currentTarget, currentPosition.x);

        if (currentPosition == _currentTarget)
        {
            _currentTarget = ChangeTarget(currentPosition);
            IsResting = true;
        }

        return Vector2.MoveTowards(currentPosition, _currentTarget, _enemy.MoveSpeed * Time.deltaTime);
    }

    private Vector2 ChangeTarget(Vector2 currentPosition)
    {
        Vector2 leftPosition = new Vector2(_startPosition.x - _offset, _startPosition.y);
        Vector2 rightPosition = new Vector2(_startPosition.x + _offset, _startPosition.y);

        if (currentPosition == leftPosition)
            return rightPosition;
        else
            return leftPosition;
    }
}