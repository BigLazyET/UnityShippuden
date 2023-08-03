namespace Assets.Scripts.Player
{
    public enum PlayerStateType
    {
        Grounded,
        Ability,
        TouchingWall,
        Idle,
        Move,
        CrouchIdle,
        CrouchMove,
        Jump,
        InAir,
        Dash,
        PrimaryAttack,
        SecondaryAttack,
        WallJump,
        LedgeClimb,
        /// <summary>
        /// 击晕，晕厥状态
        /// </summary>
        Stun
    }
}
