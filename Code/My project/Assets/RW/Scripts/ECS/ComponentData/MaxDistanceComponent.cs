using Unity.Entities;
using UnityEngine;

// 升级链接：https://docs.unity3d.com/Packages/com.unity.entities@1.0/manual/upgrade-guide.html
// GenerateAuthoringComponent 标签已经废弃，不再使用

public struct MaxDistanceComponent : IComponentData
{
    public float allowedDistance;
}

public class MaxDistanceComponentAuthoring : MonoBehaviour
{
    public float allowedDistance;   // 这个反映的是MaxDistanceComponent的字段，不过不是必须的

    //// TODO: 其实不必专门写这么一个Baker，因为ECS框架默认自带了很多转换系统帮我们去做GameObject转换成Entity，其本质是把GameObject的属性或字段转换成Entity中的Component
    //// 在GameObject转换期间，各种转换系统会处理它们识别的MonoBehaviour组件，然后将它们转换为基于ECS的components
    //// 例如，一个Unity.Transforms转换系统将检查该UnityEngine.Transform组件并添加ECS component（例如LocalToWorld）来替换它

    //// 还有一个原因：不必写这么一个Baker，但凡涉及到Baker的，那就涉及到性能提升，将多个Component内容合并到一个Component已达到性能最大化
    //// 因为 通常，最方便编写的数据布局不是运行时最高效的数据布局
    //// 自定义一个Baker，就必须写对应的转换系统BakerSystem，从而让Unity启动一个实现GameObject到Entity的System
    //class Baker : Baker<MaxDistanceComponentAuthoring>
    //{
    //    public override void Bake(MaxDistanceComponentAuthoring authoring)
    //    {
    //        var entity = GetEntity(TransformUsageFlags.None);
    //        AddComponent(entity, new MaxDistanceComponent { allowedDistance = authoring.allowedDistance });
    //    }
    //}
}


// TODO: 不确定是否单独把Baker拿出来，很多例子直接在Authoring类中实现的，如上
///// <summary>
///// Baker
///// 老的转换系统已经被Baking取代
///// IConvertGameObjectToEntity已经废弃，现在通过Baker实现
///// </summary>
//public class MaxDistanceCompoonentAuthoringBaker : Baker<MaxDistanceComponentAuthoring>
//{
//    public override void Bake(MaxDistanceComponentAuthoring authoring)
//    {
//        // EntityQueryDescBuilder和GetEntityQuery 已经废弃，请用EntityQueryBuilder代替
//        // var query = new EntityQueryBuilder(Allocator.Temp).WithAll<LocalTransform, LocalToWorld>().Build();

//        // TODO: 如何确定这里的entity
//        // 这里表示，当MaxDistanceCompoonentAuthoring附加到GameObject上，对GameObject的要求：这里对GameObject么有要求，它不一定需要有任何变换组件
//        var entity = GetEntity(TransformUsageFlags.None);

//        AddComponent(entity, new MaxDistanceComponent { allowedDistance = authoring.allowedDistance });
//    }
//}