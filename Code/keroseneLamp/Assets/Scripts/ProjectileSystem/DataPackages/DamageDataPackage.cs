using System;
using UnityEngine;

namespace Assets.Scripts.ProjectileSystem
{
    [Serializable]
    public class DamageDataPackage : ProjectileDataPackage
    {
        [field: SerializeField] public float Amount { get; private set; }
    }
}
