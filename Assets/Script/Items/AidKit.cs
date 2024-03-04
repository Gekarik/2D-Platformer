using System;
using UnityEngine;

public class AidKit : MonoBehaviour, ICollectible
{
    [SerializeField] public float HealPoints { get; private set; }
    public event Action<ICollectible> Collected;

    public void RespondToCollection()
    {
        Collected?.Invoke(this);
        Destroy(gameObject);
    }
}
