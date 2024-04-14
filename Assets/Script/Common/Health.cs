using System;
using UnityEngine;

public class Health : MonoBehaviour
{
    [field:SerializeField] public float Max { get; private set; } = 100f;
    [field:SerializeField] public float Current { get; private set; }

    public event Action Died;
    public event Action Hited;
    public event Action Changed;

    private void Start()
    {
        Current = Max;
    }

    private void OnValidate()
    {
        if (Current >= Max)
            Current = Max;
    }

    public void Heal(float healPoints)
    {
        if (Current < Max && healPoints > 0)
        {
            if (Current + healPoints >= Max)
                Current = Max;
            else
                Current += healPoints;

            Changed?.Invoke();
        }
    }

    public void TakeDamage(float damagePoint)
    {
        if (damagePoint > 0 && Current > 0)
        {
            Current -= damagePoint;
            Hited?.Invoke();
            Changed?.Invoke();
        }

        if (Current <= 0)
            Die();
    }

    public void Die()
    {
        Current = 0;
        Died?.Invoke();
        enabled = false;
    }
}
