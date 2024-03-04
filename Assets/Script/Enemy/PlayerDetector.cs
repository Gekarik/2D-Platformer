using UnityEngine;

public class PlayerDetector : MonoBehaviour
{
    public bool IsDetected { get; private set; }

    private void OnTriggerEnter2D(Collider2D other)
    {
        bool isPlayer = other.TryGetComponent(out Player _player);

        if (isPlayer)
            IsDetected = true;
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        bool isPlayer = other.TryGetComponent(out Player _player);

        if (isPlayer)
            IsDetected = false;
    }
}
