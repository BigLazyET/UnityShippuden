using System;
using UnityEngine;

namespace Assets.Scripts.EnemySystem
{
    [Serializable]
    public class EntityData : EnemyComponentData
    {
        [field: SerializeField] public float MaxHealth { get; private set; } = 30f;
        [field: SerializeField] public float DamageHopSpeed { get; private set; } = 3f;
        [field: SerializeField] public float WallCheckDistance { get; private set; } = 0.2f;
        [field: SerializeField] public float LedgeCheckDistance { get; private set; } = 0.4f;
        [field: SerializeField] public float GroundCheckRadius { get; private set; } = 0.3f;
        [field: SerializeField] public float MinAgroDistance { get; private set; } = 3f;
        [field: SerializeField] public float MaxAgroDistance { get; private set; } = 4f;
        [field: SerializeField] public float StunResistance { get; private set; } = 3f;
        [field: SerializeField] public float StunRecoveryTime { get; private set; } = 2f;
        [field: SerializeField] public float CloseRangeActionDistance { get; private set; } = 1f;
        [field: SerializeField] public GameObject HitParticle { get; private set; }
        [field: SerializeField] public LayerMask WhatIsGround { get; private set; }
        [field: SerializeField] public LayerMask WhatIsPlayer { get; private set; }
    }
}
