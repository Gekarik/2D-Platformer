using UnityEngine;

[RequireComponent(typeof(Animator))]
public class AnimatorController : MonoBehaviour
{
    private Animator _animator;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    public void SetWalking(bool isWalking) => _animator.SetBool(AnimatorData.Params.IsWalking, isWalking);

    public void SetAttack() => _animator.SetTrigger(AnimatorData.Params.Attack);

    public void SetDie() => _animator.SetTrigger(AnimatorData.Params.Die);

    public void SetHurt() => _animator.SetTrigger(AnimatorData.Params.Hurt);
}
