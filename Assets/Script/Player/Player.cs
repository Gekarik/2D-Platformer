using UnityEngine;

[RequireComponent(typeof(InputReader), typeof(Mover), typeof(AnimatorController))]
[RequireComponent(typeof(PlayerHealth))]
public class Player : MonoBehaviour
{
    private float _movementInput;
    private bool _isJumping;

    private InputReader _inputReader;
    private Mover _mover;
    private AnimatorController _animatorController;
    private PlayerHealth _playerHealth;

    private void OnEnable()
    {
        _playerHealth.Died += HandleDeath;
    }

    private void OnDisable()
    {
        _playerHealth.Died -= HandleDeath;
    }

    private void Awake()
    {
        _inputReader = GetComponent<InputReader>();
        _mover = GetComponent<Mover>();
        _animatorController = GetComponent<AnimatorController>();
        _playerHealth = GetComponent<PlayerHealth>();
    }

    private void Update()
    {
        _movementInput = _inputReader.GetHorizontalMovement();
        _isJumping = _inputReader.GetJumpMovement();

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

    private void HandleDeath()
    {
        _animatorController.SetDie();
        enabled = false;
    }
}
