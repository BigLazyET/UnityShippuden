using System;
using UnityEngine;

namespace Assets.Scripts.EnemySystem
{
    [Serializable]
    public class DodgeData : EnemyComponentData
    {
        [field: SerializeField] public float DodgeSpeed { get; private set; } = 10f;
        [field: SerializeField] public float DodgeTime { get; private set; } = 0.2f;
        [field: SerializeField] public float DodgeCooldown { get; private set; } = 2f;
        [field: SerializeField] public Vector2 DodgeAngle { get; private set; }
    }
}
