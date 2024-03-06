using System;
using UnityEngine;

public class Coin : MonoBehaviour, ICollectible
{
    [field: SerializeField] public int Cost { get; private set; }

    public event Action<ICollectible> Collected;

    public void RespondToCollection() => Collected?.Invoke(this);
}
