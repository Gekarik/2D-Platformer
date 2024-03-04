using UnityEngine;

[RequireComponent(typeof(InputReader), typeof(Mover), typeof(AnimatorController))]
[RequireComponent(typeof(PlayerHealth), typeof(PlayerCombat))]
public class Player : MonoBehaviour
{
    private float _movementInput;
    private bool _isJumping;
    private bool _isAttack;

    private InputReader _inputReader;
    private Mover _mover;
    private AnimatorController _animatorController;
    private PlayerHealth _playerHealth;
    private PlayerCombat _playerCombat;

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
        _playerHealth = GetComponent<PlayerHealth>();
        _playerCombat = GetComponent<PlayerCombat>();
    }

    private void Update()
    {
        _movementInput = _inputReader.GetHorizontalMovement();
        _isJumping = _inputReader.GetJumpMovement();
        _isAttack = _inputReader.GetLeftClick();

        if (_isAttack)
        {
            _animatorController.SetAttack();
            _playerCombat.Attack();
        }

        if (_movementInput == 0)
            _animatorController.SetWalking(false);
        else
            _animatorController.SetWalking(true);
    }

    private void FixedUpdate()
    {
        if (_isJumping)
            _mover.Jump();

        if (_movementInput != 0)
            _mover.Move(_movementInput);
    }

    private void HandleHit() => _animatorController.SetHurt();

    private void HandleDeath()
    {
        _animatorController.SetDie();
        enabled = false;
    }
}
