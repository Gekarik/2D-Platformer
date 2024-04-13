using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(AnimatorController), typeof(BoxCollider2D))]
[RequireComponent(typeof(Health), typeof(Combat), typeof(Patroller))]
public class Enemy : MonoBehaviour
{
    [field: SerializeField] public float MoveSpeed { get; private set; }
    [SerializeField] private Rigidbody2D _player;
    [SerializeField] private PlayerDetector _playerDetector;

    private Quaternion TurnLeft = Quaternion.Euler(0f, 180f, 0f);
    private Quaternion TurnRight = Quaternion.identity;
    private float _inaccuracy = 1.5f;

    private Rigidbody2D _rigidbody2D;
    private Patroller _patroller;
    private AnimatorController _animatorController;
    private Health _health;
    private Combat _combat;

    private void OnEnable()
    {
        _health.Died += HandleDeath;
        _health.Hited += HandleHit;
    }

    private void OnDisable()
    {
        _health.Died -= HandleDeath;
        _health.Hited -= HandleHit;
    }

    private void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _patroller = GetComponent<Patroller>();
        _animatorController = GetComponent<AnimatorController>();
        _health = GetComponent<Health>();
        _combat = GetComponent<Combat>();

        _patroller.SetStartPositions(_rigidbody2D.position);
    }

    private void FixedUpdate()
    {
        if (_playerDetector.IsDetected)
        {
            _animatorController.SetWalking(true);
            FollowPlayer();
        }
        else
        {
            if (_patroller.IsResting)
            {
                _animatorController.SetWalking(false);
                _patroller.Rest();
            }
            else
            {
                _animatorController.SetWalking(true);
                _rigidbody2D.position = _patroller.Patrol(_rigidbody2D.position);
            }
        }
    }

    private void HandleDeath()
    {
        _animatorController.SetDie();
        TryGetComponent(out BoxCollider2D boxCollider2D);
        boxCollider2D.enabled = false;
        enabled = false;
    }

    private void HandleHit() => _animatorController.SetHurt();

    public void FollowPlayer()
    {
        Flip(_player.position, _rigidbody2D.position.x);

        if (Vector2.Distance(_rigidbody2D.position, _player.position) > _inaccuracy)
        {
            _rigidbody2D.position = Vector2.MoveTowards(new Vector2(_rigidbody2D.position.x,0), new Vector2(_player.position.x, 0) , MoveSpeed * Time.deltaTime);
        }
        else
        {
            _animatorController.SetWalking(false);

            if (_combat.CanAttack)
            {
                _animatorController.SetAttack();
                _combat.Attack();
            }
        }
    }

    public void Flip(Vector2 target, float currentXPosition) => transform.localRotation = target.x - currentXPosition > 0 ? TurnLeft : TurnRight;
}