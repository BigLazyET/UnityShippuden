using System;
using UnityEngine;

namespace Assets.Scripts.EnemySystem
{
    [Serializable]
    public class ChargeData : EnemyComponentData
    {
        [field: SerializeField] public float ChargeSpeed { get; private set; } = 6f;

        [field: SerializeField] public float ChargeTime { get; private set; }
    }
}
