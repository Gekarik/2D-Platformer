using System;
using UnityEngine;

public class Coin : MonoBehaviour, ICollectible
{
    public event Action<ICollectible> Collected;
    [field: SerializeField] public int Cost { get; private set; }

    public void RespondToCollection()
    {
        Collected?.Invoke(this);
        Destroy(gameObject);
    }
}
