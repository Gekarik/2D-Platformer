using UnityEngine;

public class PlayerDetector : MonoBehaviour
{
    public bool IsDetected { get; private set; }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent(out Player _player))
            IsDetected = true;
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.TryGetComponent(out Player _player))
            IsDetected = true; 
    }
}
