using System;
using UnityEngine;

public class Health : MonoBehaviour
{
    [field:SerializeField] public float Max { get; private set; } = 100f;
    [field:SerializeField] public float Current { get; private set; }

    private float min = 0f;

    public event Action Died;
    public event Action Hited;
    public event Action Changed;

    private void Start()
    {
        Current = Max;
    }

    private void OnValidate()
    {
        Current = Mathf.Clamp(Current, 0, Max);
    }

    public void Heal(float healPoints)
    {
        if (healPoints > 0)
        {
            Current = Mathf.Clamp(Current + healPoints, min, Max);
            Changed?.Invoke();
        }
    }

    public void TakeDamage(float damagePoints)
    {
        if (damagePoints > 0)
        {
            Current = Mathf.Clamp(Current - damagePoints, min, Max);
            Hited?.Invoke();
            Changed?.Invoke();

            if (Current <= 0)
                Die();
        }
    }

    public void Die()
    {
        Died?.Invoke();
        enabled = false;
    }
}
