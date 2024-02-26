using System;
using UnityEngine;

public class CoinGrabber : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        bool isCoin = other.TryGetComponent(out Coin coin);

        if (isCoin)
            Destroy(coin.gameObject);
    }
}
