using Unity.Entities;
using UnityEngine;

public struct FXComponent : IComponentData
{
    public Entity Value;
}

public class FXComponentAuthoring : MonoBehaviour
{
    public Entity Value;
}
