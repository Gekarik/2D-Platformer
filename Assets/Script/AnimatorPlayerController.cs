using UnityEngine;

public class AnimatorPlayerController : MonoBehaviour
{
    public static class Params
    {
        public const string Speed = nameof(Speed);
    }

    public static class States
    {
        public const string Idle = nameof(Idle);
        public const string Walk = nameof(Walk);
    }
}
