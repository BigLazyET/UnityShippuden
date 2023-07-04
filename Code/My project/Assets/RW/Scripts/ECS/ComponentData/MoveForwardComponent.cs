using Unity.Entities;
using UnityEngine;

public struct MoveForwardComponent : IComponentData
{
    public float speed;
}

public class MoveForwardComponentAuthoring: MonoBehaviour
{
    public float speed;
}