using UnityEngine;

public class AnimatorData : MonoBehaviour
{
    public static class Params
    {
        public const string IsWalking = nameof(IsWalking);
        public const string Attack = nameof(Attack);
        public const string Die = nameof(Die);
        public const string Hurt = nameof(Hurt);
        public const string AirSpeedY = nameof(AirSpeedY);
        public const string IsGrounded = nameof(IsGrounded);
    }

    public static class States
    {
        public const string Idle = nameof(Idle);
        public const string Walk = nameof(Walk);
    }
}
