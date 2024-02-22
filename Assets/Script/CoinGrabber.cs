using System;
using UnityEngine;

public class CoinGrabber : MonoBehaviour
{
    public static event Action<Coin> OnCoinCollected;
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponent<Coin>() != null)
        {
            OnCoinCollected?.Invoke(other.GetComponent<Coin>());
            Destroy(other.gameObject);
        }   
    }
}
