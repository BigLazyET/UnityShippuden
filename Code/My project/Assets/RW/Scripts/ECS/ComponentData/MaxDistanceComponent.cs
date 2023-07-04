using Unity.Entities;
using UnityEngine;

// �������ӣ�https://docs.unity3d.com/Packages/com.unity.entities@1.0/manual/upgrade-guide.html
// GenerateAuthoringComponent ��ǩ�Ѿ�����������ʹ��

public struct MaxDistanceComponent : IComponentData
{
    public float allowedDistance;
}

public class MaxDistanceComponentAuthoring : MonoBehaviour
{
    public float allowedDistance;   // �����ӳ����MaxDistanceComponent���ֶΣ��������Ǳ����

    //// TODO: ��ʵ����ר��д��ôһ��Baker����ΪECS���Ĭ���Դ��˺ܶ�ת��ϵͳ������ȥ��GameObjectת����Entity���䱾���ǰ�GameObject�����Ի��ֶ�ת����Entity�е�Component
    //// ��GameObjectת���ڼ䣬����ת��ϵͳ�ᴦ������ʶ���MonoBehaviour�����Ȼ������ת��Ϊ����ECS��components
    //// ���磬һ��Unity.Transformsת��ϵͳ������UnityEngine.Transform��������ECS component������LocalToWorld�����滻��

    //// ����һ��ԭ�򣺲���д��ôһ��Baker�������漰��Baker�ģ��Ǿ��漰�����������������Component���ݺϲ���һ��Component�Ѵﵽ�������
    //// ��Ϊ ͨ��������д�����ݲ��ֲ�������ʱ���Ч�����ݲ���
    //// �Զ���һ��Baker���ͱ���д��Ӧ��ת��ϵͳBakerSystem���Ӷ���Unity����һ��ʵ��GameObject��Entity��System
    //class Baker : Baker<MaxDistanceComponentAuthoring>
    //{
    //    public override void Bake(MaxDistanceComponentAuthoring authoring)
    //    {
    //        var entity = GetEntity(TransformUsageFlags.None);
    //        AddComponent(entity, new MaxDistanceComponent { allowedDistance = authoring.allowedDistance });
    //    }
    //}
}


// TODO: ��ȷ���Ƿ񵥶���Baker�ó������ܶ�����ֱ����Authoring����ʵ�ֵģ�����
///// <summary>
///// Baker
///// �ϵ�ת��ϵͳ�Ѿ���Bakingȡ��
///// IConvertGameObjectToEntity�Ѿ�����������ͨ��Bakerʵ��
///// </summary>
//public class MaxDistanceCompoonentAuthoringBaker : Baker<MaxDistanceComponentAuthoring>
//{
//    public override void Bake(MaxDistanceComponentAuthoring authoring)
//    {
//        // EntityQueryDescBuilder��GetEntityQuery �Ѿ�����������EntityQueryBuilder����
//        // var query = new EntityQueryBuilder(Allocator.Temp).WithAll<LocalTransform, LocalToWorld>().Build();

//        // TODO: ���ȷ�������entity
//        // �����ʾ����MaxDistanceCompoonentAuthoring���ӵ�GameObject�ϣ���GameObject��Ҫ�������GameObjectô��Ҫ������һ����Ҫ���κα任���
//        var entity = GetEntity(TransformUsageFlags.None);

//        AddComponent(entity, new MaxDistanceComponent { allowedDistance = authoring.allowedDistance });
//    }
//}