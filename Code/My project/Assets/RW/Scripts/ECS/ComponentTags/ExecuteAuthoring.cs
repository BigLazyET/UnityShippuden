using Unity.Entities;
using UnityEngine;

public class ExecuteAuthoring : MonoBehaviour
{
    public bool Cleanup;
    public bool Destroyed;
    public bool Enemy;

    class Baker : Baker<ExecuteAuthoring>
    {
        public override void Bake(ExecuteAuthoring authoring)
        {
            var entity = GetEntity(TransformUsageFlags.None);

            if (authoring.Cleanup) AddComponent<CleanupTag>(entity);
            if (authoring.Destroyed) AddComponent<DestroyedTag>(entity);
            if (authoring.Enemy) AddComponent<EnemyTag>(entity);
        }
    }
}
