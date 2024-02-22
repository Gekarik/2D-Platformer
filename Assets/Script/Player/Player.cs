using UnityEngine;

[RequireComponent(typeof(InputReader),typeof(Mover),typeof(AnimatorController))]
public class Player : MonoBehaviour
{
    [SerializeField] private float _moveSpeed;
    [SerializeField] private float _jumpForce;

    private bool _isJumping;
    private float _movementInput;

    private InputReader _inputReader;
    private Mover _mover;
    private AnimatorController _animatorController;

    private void Awake()
    {
        _inputReader = GetComponent<InputReader>();
        _mover = GetComponent<Mover>();
        _animatorController = GetComponent<AnimatorController>();
    }

    private void FixedUpdate()
    {
        if (_isJumping)
            _mover.Jump(_jumpForce);

        if (_movementInput != 0)
        {
            _mover.Move(_movementInput * _moveSpeed);
            _animatorController.SetWalking(true);
        }
        else
        {
            _animatorController.SetWalking(false);
        }
    }

    private void Update()
    {
        _movementInput = _inputReader.GetHorizontalMovement();
        _isJumping = _inputReader.GetJumpMovement();
    }
}
