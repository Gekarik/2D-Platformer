using System.Collections;
using UnityEngine;

public class Combat : MonoBehaviour
{
    [SerializeField] private float _damage = 100f;
    [SerializeField] private Transform _attackPoint;
    [SerializeField] private float _attackPointRadius = 0.5f;
    [SerializeField] private float _attackRate;
    [SerializeField] private LayerMask _enemyMask;

    public bool CanAttack { get; private set; } = true;
    private Coroutine _attackCooldownCoroutine;
    private float _cooldownAttackTime;
    private WaitForSeconds _wait;

    private void Start()
    {
        _cooldownAttackTime = 1f / _attackRate;
        _wait = new WaitForSeconds(_cooldownAttackTime);
    }

    public void Attack()
    {
        if (CanAttack)
        {
            Collider2D[] hitted = Physics2D.OverlapCircleAll(_attackPoint.position, _attackPointRadius, _enemyMask);

            foreach (Collider2D other in hitted)
            {
                if (other.TryGetComponent(out Health enemyHealth))
                    enemyHealth.TakeDamage(_damage);
            }

            if (_attackCooldownCoroutine != null)
                StopCoroutine(_attackCooldownCoroutine);

            _attackCooldownCoroutine = StartCoroutine(AttackCooldown());
        }
    }

    private IEnumerator AttackCooldown()
    {
        CanAttack = false;
        yield return _wait;
        CanAttack = true;
    }

    private void OnDrawGizmosSelected()
    {
        if (_attackPoint == null)
            return;

        Gizmos.DrawWireSphere(_attackPoint.position, _attackPointRadius);
    }
}