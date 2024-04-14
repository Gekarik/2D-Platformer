using System.Collections;
using UnityEngine;

public class Combat : MonoBehaviour
{
    [SerializeField] private float _meleeDamage = 25f;
    [SerializeField] private float _attackRate;

    [SerializeField] private float _castRate;
    [SerializeField] private float _abilityDamage = 12.5f;
    [SerializeField] private float _vampirismDuration = 6.0f;

    [SerializeField] private Transform _attackPoint;
    [SerializeField] private float _attackPointRadius = 0.5f;
    [SerializeField] private LayerMask _enemyMask;

    public bool CanCast { get; private set; } = true;
    public bool CanAttack { get; private set; } = true;

    private Health _selfHealth;
    private WaitForSeconds _attackCooldownTime;
    private WaitForSeconds _castCooldownTime;

    private enum Mode
    {
        Attack = 1,
        Cast
    };

    private void Awake()
    {
        _selfHealth = GetComponent<Health>();
        _attackCooldownTime = new WaitForSeconds(1f / _attackRate);
        _castCooldownTime = new WaitForSeconds(1f / _castRate);
    }

    private void OnEnable()
    {
        _selfHealth.Died += HandleDeath;
    }

    private void OnDisable()
    {
        _selfHealth.Died -= HandleDeath;
    }

    private void HandleDeath()
    {
        StopAllCoroutines();
    }

    public void Attack()
    {
        if (CanAttack)
        {
            ProcessEnemies(_meleeDamage, Mode.Attack);
            StartCoroutine(Cooldown(Mode.Attack));
        }
    }

    public void SuckBlood()
    {
        if (CanCast)
        {
            StartCoroutine(SuckBloodRoutine());
            StartCoroutine(Cooldown(Mode.Cast));
        }
    }

    private IEnumerator SuckBloodRoutine()
    {
        float timePassed = 0f;

        while (timePassed < _vampirismDuration)
        {
            float damageThisFrame = Time.deltaTime * _abilityDamage; 
            ProcessEnemies(damageThisFrame, Mode.Cast);
            timePassed += Time.deltaTime;
            yield return null;
        }
    }

    private void ProcessEnemies(float damageAmount, Mode mode)
    {
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(_attackPoint.position, _attackPointRadius, _enemyMask);

        foreach (Collider2D enemy in hitEnemies)
        {
            if (enemy.TryGetComponent(out Health enemyHealth))
            {
                enemyHealth.TakeDamage(damageAmount);

                if (mode == Mode.Cast)
                    _selfHealth.Heal(damageAmount);
            }
        }
    }

    private IEnumerator Cooldown(Mode mode)
    {
        switch (mode)
        {
            case Mode.Attack:
                CanAttack = false;
                yield return _attackCooldownTime;
                CanAttack = true;
                break;

            case Mode.Cast:
                CanCast = false;
                yield return _castCooldownTime;
                CanCast = true;
                break;
        }
    }

    private void OnDrawGizmosSelected()
    {
        if (_attackPoint != null)
            Gizmos.DrawWireSphere(_attackPoint.position, _attackPointRadius);
    }
}
