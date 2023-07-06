using Unity.Burst;
using Unity.Entities;

namespace PlayerController
{
    /// <summary>
    /// 从全局的Singleton PlayerController Input Component Data中获取Input信息
    /// 应用到Player的运动信息中
    /// </summary>
    [BurstCompile]
    public partial struct PlayerControllerInternalJob : IJobEntity
    {
        public Input input;

        public void Execute(ref PlayerControllerInternal playerControllerInternal)
        {
            playerControllerInternal.Input.movement = input.movement;
            playerControllerInternal.Input.looking = input.looking;
            if (input.jumped != 0)
                playerControllerInternal.Input.jumped = 1;   
        }
    }
}
