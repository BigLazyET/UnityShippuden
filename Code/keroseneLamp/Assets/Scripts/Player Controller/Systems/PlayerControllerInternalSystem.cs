using Common;
using Unity.Burst;
using Unity.Entities;

namespace PlayerController
{
    /// <summary>
    /// 从全局的Singleton PlayerController Input Component Data中获取Input信息
    /// 应用到Player的运动信息中
    /// </summary>
    [RequireMatchingQueriesForUpdate]
    [UpdateInGroup(typeof(InitializationSystemGroup))]
    [UpdateAfter(typeof(InputGatheringSystem))]
    public partial struct PlayerControllerInternalSystem : ISystem
    {
        [BurstCompile]
        public void OnCreate(ref SystemState state)
        {
            state.RequireForUpdate<Input>();
        }

        [BurstCompile]
        public void OnUpdate(ref SystemState state)
        {
            var job = new PlayerControllerInternalJob
            {
                input = SystemAPI.GetSingleton<Input>()
            };

            state.Dependency = job.ScheduleParallel(state.Dependency);
        }
    }
}
