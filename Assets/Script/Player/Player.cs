using UnityEngine;

[RequireComponent(typeof(InputReader), typeof(Mover), typeof(AnimatorController))]
[RequireComponent(typeof(Health), typeof(Combat))]
public class Player : MonoBehaviour
{
    private float _movementInput;
    private bool _isJumping;
    private bool _isAttack;
    private bool _isVampirismCast;

    private InputReader _inputReader;
    private Mover _mover;
    private AnimatorController _animatorController;
    private Health _playerHealth;
    private Combat _playerCombat;

    private void OnEnable()
    {
        _playerHealth.Died += HandleDeath;
        _playerHealth.Hited += HandleHit;
    }

    private void OnDisable()
    {
        _playerHealth.Died -= HandleDeath;
        _playerHealth.Hited -= HandleHit;
    }

    private void Awake()
    {
        _inputReader = GetComponent<InputReader>();
        _mover = GetComponent<Mover>();
        _animatorController = GetComponent<AnimatorController>();
        _playerHealth = GetComponent<Health>();
        _playerCombat = GetComponent<Combat>();
    }

    private void Update()
    {
        _movementInput = _inputReader.GetHorizontalMovement();
        _isJumping = _inputReader.GetJumpMovement();
        _isAttack = _inputReader.GetLeftClick();
        _isVampirismCast = _inputReader.GetRightClick();

        if (_isAttack && _playerCombat.CanAttack)
        {
            _animatorController.SetAttack();
            _playerCombat.Attack();
        }

        if (_isVampirismCast && _playerCombat.CanCast)
            _playerCombat.SuckBlood();

        if (_movementInput == 0)
            _animatorController.SetWalking(false);
        else
            _animatorController.SetWalking(true);
    }

    private void FixedUpdate()
    {
        if (_isJumping)
            _mover.Jump();

        _animatorController.Jump(_mover.AirSpeedY, _mover.IsGrounded);

        if (_movementInput != 0)
            _mover.Move(_movementInput);
    }

    private void HandleHit() => _animatorController.SetHurt();

    private void HandleDeath()
    {
        enabled = false;
        _animatorController.SetDie();
        _playerHealth.enabled = false;
        _playerCombat.enabled = false;
    }
}
