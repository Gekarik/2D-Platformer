using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    [SerializeField] private float _playerDamage = 100f;
    [SerializeField] private Transform attackPoint;
    [SerializeField] private float attackPointRadius = 0.5f;

    private bool _isAttack;

    private InputReader _inputReader;
    private AnimatorController _animatorController;

    private void Start()
    {
        _inputReader = GetComponent<InputReader>();
        _animatorController = GetComponent<AnimatorController>();
    }

    private void Update()
    {
        _isAttack = _inputReader.GetLeftClick();

        if (_isAttack)
        {
            _animatorController.SetAttack();
            Attack();
        }
    }

    public void Attack()
    {
        Collider2D[] hitted = Physics2D.OverlapCircleAll(attackPoint.position, attackPointRadius);

        foreach (Collider2D other in hitted)
        {
            if (other.TryGetComponent(out EnemyHealth enemyHealth))
                enemyHealth.TakeDamage(_playerDamage);
        }
    }

    private void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
            return;

        Gizmos.DrawWireSphere(attackPoint.position, attackPointRadius);
    }
}
