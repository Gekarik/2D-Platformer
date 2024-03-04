using System;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public event Action Died;
    public event Action Hited;

    [SerializeField] private float _health = 100f;
    [SerializeField] private float _maxHealth = 100f;

    private void Start()
    {
        _health = _maxHealth;
    }

    public void Heal(float healPoints)
    {
        if (_health < _maxHealth)
            _health += healPoints;
    }

    public void TakeDamage(float damagePoint)
    {
        _health -= damagePoint;
        Hited?.Invoke();

        if (_health <= 0)
        {
            _health = 0;
            Died?.Invoke();
            enabled = false;
        }
    }
}
