using Unity.Entities;
using UnityEngine;

public struct FXComponent : IComponentData
{
    public Entity Value;
}

public class FXComponentAuthoring : MonoBehaviour
{
    public Entity Value;

    class Baker : Baker<FXComponentAuthoring>
    {
        public override void Bake(FXComponentAuthoring authoring)
        {
            var entity = GetEntity(TransformUsageFlags.None);
            AddComponent(entity, new FXComponent { Value = authoring.Value });
        }
    }
}
