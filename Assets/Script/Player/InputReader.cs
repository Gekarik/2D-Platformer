using UnityEngine;

public class InputReader : MonoBehaviour
{
    private const string Horizontal = nameof(Horizontal);
    private const string Vertical = nameof(Vertical);
    private const string Jump = nameof(Jump);
    private const string Fire1 = nameof(Fire1);
    private const string Fire2 = nameof(Fire2);

    public float GetHorizontalMovement() => Input.GetAxis(Horizontal);

    public bool GetJumpMovement() => Input.GetAxis(Jump) > 0;

    public bool GetLeftClick() => Input.GetButtonDown(Fire1);

    public bool GetRightClick() => Input.GetButtonDown(Fire2);
}
