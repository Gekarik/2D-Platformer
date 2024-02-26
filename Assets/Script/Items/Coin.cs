using System;
using UnityEngine;

public class Coin : MonoBehaviour
{
    public event Action<Coin> OnCoinDestroy;

    private void OnDestroy()
    {
        OnCoinDestroy?.Invoke(this);
    }
}
