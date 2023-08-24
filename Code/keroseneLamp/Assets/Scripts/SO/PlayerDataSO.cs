using UnityEngine;

namespace Assets.Scripts.SO
{
    [CreateAssetMenu(fileName = "newPlayerData", menuName = "Data/Player Data/Basic Player Data")]
    public class PlayerDataSO : ScriptableObject
    {
        [Header("Move State")]
        public float movementVelocity = 10f;    // 移动速度

        [Header("Jump State")]
        public float jumpVelocity = 15f;    // 跳跃速度
        public int amountOfJumps = 1;   // 跳跃次数

        [Header("Wall Jump State")]
        public float wallJumpVelocity = 20; // 墙壁跳跃速度
        public float wallJumpTime = 0.4f;   // 
        public Vector2 wallJumpAngle = new Vector2(1, 2);   // 墙壁跳跃角度

        [Header("In Air State")]
        public float coyoteTime = 0.2f; // 土狼时间：在判定玩家脱离平台并处于下落状态下，设置一个计时器（比如0.2秒），在这个计时器的持续时间内，玩家依旧可以进行跳跃指令，就像还在平台上一样
        public float variableJumpHeightMultiplier = 0.5f;   // 

        [Header("Wall Slide State")]
        public float wallSlideVelocity = 3f;    // 墙壁滑落速度

        [Header("Wall Climb State")]
        public float wallClimbVelocity = 3f;    // 墙壁攀爬速度

        [Header("Ledge Climb State")]
        public Vector2 startOffset;     // 可攀爬的偏移/距离? TODO
        public Vector2 stopOffset;

        [Header("Dash State")]
        public float dashCooldown = 0.5f;   // 冲刺冷却时间
        public float maxHoldTime = 1f;  // 冲刺按键按住最大持续时间
        public float holdTimeScale = 0.25f;
        public float dashTime = 0.2f;   // 冲刺时间
        public float dashVelocity = 30f;    // 冲刺速度
        public float drag = 10f;
        public float dashEndYMultiplier = 0.2f;
        public float distBetweenAfterImages = 0.5f; // 冲刺多个sprite之间的距离

        [Header("Crouch States")]
        public float crouchMovementVelocity = 5f;   // 蹲下时移动速度
        public float crouchColliderHeight = 0.8f;   // 蹲下时碰撞器高度
        public float standColliderHeight = 1.6f;    // 站起时碰撞器高度

        [Header("Stun State")]
        public float stunTime = 2f; // 晕眩时间(耐毒值消耗结束后)
    }
}
