using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;

[WithAll(typeof(MoveForwardComponent))]
public partial struct MovementSystem : ISystem
{
    [BurstCompile]
    public void OnUpdate(ref SystemState state)
    {
        // take SystemAPI.QueryBuilder way
        //var query = SystemAPI.QueryBuilder().WithAll<RefRO<MoveForwardComponent>, RefRW<LocalTransform>>().Build();
        //using var entities = query.ToEntityArray(Allocator.TempJob);

        //foreach (var entity in entities)
        //{
        //    var speed = SystemAPI.GetComponent<MoveForwardComponent>(entity).speed;
        //    var transform = SystemAPI.GetComponent<LocalTransform>(entity);

        //    transform.Translate(transform.Position + speed * SystemAPI.Time.DeltaTime * math.forward(transform.Rotation));
        //}

        // take SystemAPI.Query way
        //foreach (var (transform, moveforward) in SystemAPI.Query<RefRW<LocalTransform>, RefRO<MoveForwardComponent>>())
        //{
        //    transform.ValueRW.Translate(transform.ValueRO.Position + moveforward.ValueRO.speed * SystemAPI.Time.DeltaTime * math.forward(transform.ValueRO.Rotation));
        //} 

        // take Aspect way
        foreach (var aspect in SystemAPI.Query<MoveAspect>())
        {
            aspect.Move(SystemAPI.Time.DeltaTime);
        }
    }
}

readonly partial struct MoveAspect : IAspect
{
    readonly RefRW<LocalTransform> transform;
    readonly RefRO<MoveForwardComponent> moveforward;

    public void Move(float deltaTime)
    {
        transform.ValueRW.Translate(transform.ValueRO.Position + moveforward.ValueRO.speed * deltaTime * math.forward(transform.ValueRO.Rotation));
    }
}
