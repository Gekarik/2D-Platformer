using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    private Animator _animator;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    public void SetWalking(bool isWalking) => _animator.SetBool(Params.isWalking, isWalking);

    public static class Params
    {
        public const string isWalking = nameof(isWalking);
    }

    public static class States
    {
        public const string Idle = nameof(Idle);
        public const string Walk = nameof(Walk);
    }
}