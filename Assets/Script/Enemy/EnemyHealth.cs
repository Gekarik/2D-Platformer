using System;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public Action Died;

    [SerializeField] public float Health { get; private set; }
    [SerializeField] private float _maxHealth = 100f;

    private AnimatorController _animatorController;

    public void Start()
    {
        _animatorController = GetComponent<AnimatorController>();
        Health = _maxHealth;
    }

    private void OnValidate()
    {
        if (Health >= _maxHealth)
            Health = _maxHealth;
    }

    public void TakeDamage(float damage)
    {
        if (Health > 0)
        {
            _animatorController.SetHurt();
            Health -= damage;
        }

        if (Health < 0)
        {
            Health = 0;
            Died?.Invoke();
        }
    }

    public void Die()
    {
        _animatorController.SetDie();
        enabled = false;
    }
}
