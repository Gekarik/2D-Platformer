using UnityEngine;

public class InputReader : MonoBehaviour
{
    private const string Horizontal = nameof(Horizontal);
    private const string Vertical = nameof(Vertical);
    private const string Jump = nameof(Jump);

    public float GetHorizontalMovement() => Input.GetAxis(Horizontal);

    public bool GetJumpMovement() => Input.GetAxis(Jump) > 0;
}
