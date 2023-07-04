using Unity.Entities;
using UnityEngine;

public struct LifetimeComponent : IComponentData
{
    public float timeAlive;
}

public class LifetimeComponentAuthoring: MonoBehaviour
{
    public float timeAlive;
}
