using UnityEngine;

[RequireComponent(typeof(InputReader),typeof(Mover),typeof(PlayerAnimator))]
public class Player : MonoBehaviour
{
    [SerializeField] private float _moveSpeed;
    [SerializeField] private float _jumpForce;

    private InputReader _inputReader;
    private Mover _mover;
    private PlayerAnimator _animatorController;

    private void Awake()
    {
        _inputReader = GetComponent<InputReader>();
        _mover = GetComponent<Mover>();
        _animatorController = GetComponent<PlayerAnimator>();
    }

    private void Update()
    {
        float movementInput = _inputReader.GetHorizontalMovement();
        bool isJumping = _inputReader.GetJumpMovement();

        if (isJumping)
            _mover.Jump(_jumpForce);

        if (movementInput != 0)
        {
            _mover.Move(movementInput * _moveSpeed);
            _animatorController.SetWalking(true);
        }
        else
        {
            _animatorController.SetWalking(false);
        }
    }
}
