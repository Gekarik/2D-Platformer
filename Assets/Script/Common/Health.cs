using System;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private float _current = 100f;
    [SerializeField] private float _max = 100f;

    public event Action Died;
    public event Action Hited;

    private void Start()
    {
        _current = _max;
    }

    private void OnValidate()
    {
        if (_current >= _max)
            _current = _max;
    }

    public void Heal(float healPoints)
    {
        if (_current < _max && healPoints > 0)
            _current += healPoints;
    }

    public void TakeDamage(float damagePoint)
    {
        if(damagePoint>0)
        {
            _current -= damagePoint;
            Hited?.Invoke();
        }

        if (_current <= 0)
            Die();
    }

    public void Die()
    {
        _current = 0;
        Died?.Invoke();
        enabled = false;
    }
}
