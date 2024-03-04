using UnityEngine;

public class EnemyCombat : MonoBehaviour
{
    [SerializeField] private float _damage = 10f;
    [SerializeField] private float _attackPointRadius;
    [SerializeField] private Transform _attackPoint;
    [SerializeField] private float _attackRate = 1f;

    private float _nextAttackTime =0f;
    private AnimatorController _knightAnimator;

    private void Awake()
    {
        _knightAnimator = GetComponent<AnimatorController>();
    }

    public void AttackPlayer()
    {
        if (Time.time >= _nextAttackTime)
        {
            _nextAttackTime = Time.time + 1f / _attackRate;
            _knightAnimator.SetAttack();

            Collider2D[] hitted = Physics2D.OverlapCircleAll(_attackPoint.position, _attackPointRadius);

            foreach (Collider2D other in hitted)
            {
                if (other.TryGetComponent(out PlayerHealth playerHealth))
                    playerHealth.TakeDamage(_damage);
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        if (_attackPoint == null)
            return;
     
        Gizmos.DrawWireSphere(_attackPoint.position, _attackPointRadius);
    }
}
