using System;
using UnityEngine;

namespace Assets.Scripts.ProjectileSystem
{
    [Serializable]
    public class KnockBackDataPackage : ProjectileDataPackage
    {
        [field: SerializeField] public Vector2 Angle { get; private set; }
        [field: SerializeField] public float Strength { get; private set; }
    }
}
