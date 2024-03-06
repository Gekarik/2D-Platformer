using System;
using UnityEngine;

public class AidKit : MonoBehaviour, ICollectible
{
    [field: SerializeField] public float HealPoints { get; private set; }

    public event Action<ICollectible> Collected;

    public void RespondToCollection() => Collected?.Invoke(this);
}
