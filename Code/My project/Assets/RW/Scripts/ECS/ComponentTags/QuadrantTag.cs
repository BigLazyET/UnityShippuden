using Unity.Entities;
using UnityEngine;

public struct QuadrantTag : IComponentData
{
    public QuadrantUnitType unitType;
}

public class QuadrantTagAuthoring : MonoBehaviour
{
    public QuadrantUnitType unitType;
}

public enum QuadrantUnitType
{
    Enemy,
    Bullet
}
