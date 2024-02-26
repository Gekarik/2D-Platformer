using System;
using UnityEngine;

public class CoinGrabber : MonoBehaviour
{
    public event Action<Coin> OnCoinCollected;
    private Coin _coin;

    private void OnTriggerEnter2D(Collider2D other)
    {
        bool isCoin = other.TryGetComponent(out _coin);

        if (isCoin)
        {
            OnCoinCollected?.Invoke(_coin);
            Destroy(other.gameObject);
        }
    }
}
