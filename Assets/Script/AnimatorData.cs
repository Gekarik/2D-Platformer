using UnityEngine;

public class AnimatorData : MonoBehaviour
{
    public static class Params
    {
        public const string isWalking = nameof(isWalking);
    }

    public static class States
    {
        public const string Idle = nameof(Idle);
        public const string Walk = nameof(Walk);
    }
}
