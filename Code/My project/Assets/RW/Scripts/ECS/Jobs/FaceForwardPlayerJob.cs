using Unity.Burst;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;

[BurstCompile]
public partial struct FaceForwardPlayerJob : IJobEntity
{
    public float3 playerPosition;

    public void Execute(Entity entity, ref LocalTransform transform)
    {
        var direction = playerPosition - transform.Position;
        direction.y = 0f;
        transform.Rotation = quaternion.LookRotation(direction, math.up());
    }
}
