using UnityEngine;

[RequireComponent(typeof(InputReader),typeof(Mover),typeof(AnimatorController))]
public class Player : MonoBehaviour
{
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
        float _movementInput = _inputReader.GetHorizontalMovement();
        bool _isJumping = _inputReader.GetJumpMovement();

        if (_isJumping)
            _mover.Jump();

        if (_movementInput == 0)
        {
            _animatorController.SetWalking(false);
        }
        else
        {
            _mover.Move(_movementInput);
            _animatorController.SetWalking(true);
        }
    }
}
