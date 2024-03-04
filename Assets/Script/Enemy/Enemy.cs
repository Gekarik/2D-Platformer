using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(AnimatorController), typeof(BoxCollider2D))]
[RequireComponent(typeof(EnemyHealth),typeof(EnemyCombat), typeof(Patroller))]
public class Enemy : MonoBehaviour
{
    [field: SerializeField] public float MoveSpeed { get; private set; }
    [SerializeField] private Rigidbody2D _player;
    [SerializeField] private PlayerDetector _playerDetector;

    private Quaternion TurnLeft = Quaternion.Euler(0f, 180f, 0f);
    private Quaternion TurnRight = Quaternion.identity;
    private float _offset = 1.5f;

    private Rigidbody2D _rigidbody2D;
    private Patroller _patroller;
    private AnimatorController _knightAnimator;
    private EnemyHealth _enemyHealth;
    private EnemyCombat _enemyCombat;

    private void OnEnable()
    {
        _enemyHealth.Died += HandleDeath;
    }

    private void OnDisable()
    {
        _enemyHealth.Died -= HandleDeath;
    }

    private void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _patroller = GetComponent<Patroller>();
        _knightAnimator = GetComponent<AnimatorController>();
        _enemyHealth = GetComponent<EnemyHealth>();
        _enemyCombat = GetComponent<EnemyCombat>();

        _patroller.SetStartPositions(_rigidbody2D.position);
    }

    private void FixedUpdate()
    {
        if (_playerDetector.IsDetected)
        {
            _knightAnimator.SetWalking(true);
            FollowPlayer();
        }
        else
        {
            if (_patroller.IsResting)
            {
                _knightAnimator.SetWalking(false);
                _patroller.Rest();
            }
            else
            {
                _knightAnimator.SetWalking(true);
                _rigidbody2D.position = _patroller.Patrol(_rigidbody2D.position);
            }
        }
    }

    private void HandleDeath()
    {
        _knightAnimator.SetDie();
        GetComponent<BoxCollider2D>().enabled = false;
        enabled = false;
    }

    public void FollowPlayer()
    {
        Flip(_player.position, _rigidbody2D.position.x);
        Vector2 playerPosition = CalculatePlayerPosition();

        if (_rigidbody2D.position != playerPosition)
        {
            _rigidbody2D.position = Vector2.MoveTowards(_rigidbody2D.position, playerPosition, MoveSpeed * Time.deltaTime);
        }
        else
        {
            _knightAnimator.SetWalking(false);
            _enemyCombat.AttackPlayer();
        }
    }

    public void Flip(Vector2 target, float currentXPosition)
    {
        switch (target.x - currentXPosition)
        {
            case > 0:
                transform.localRotation = TurnLeft;
                break;
            case < 0:
                transform.localRotation = TurnRight;
                break;
        }
    }

    private Vector2 CalculatePlayerPosition()
    {
        if (_player.position.x < _rigidbody2D.position.x)
            return new Vector2(_player.position.x + _offset, _rigidbody2D.position.y);
        else
            return new Vector2(_player.position.x - _offset, _rigidbody2D.position.y);
    }
}