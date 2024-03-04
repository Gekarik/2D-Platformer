using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    [SerializeField] private float _playerDamage = 100f;
    [SerializeField] private Transform attackPoint;
    [SerializeField] private float attackPointRadius = 0.5f;
    [SerializeField] private float test;

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
