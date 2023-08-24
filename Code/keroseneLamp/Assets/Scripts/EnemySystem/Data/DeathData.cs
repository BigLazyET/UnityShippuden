using System;
using UnityEngine;

namespace Assets.Scripts.EnemySystem
{
    [Serializable]
    public class DeathData : EnemyComponentData
    {
        [field: SerializeField] public GameObject DeathChunckParticle { get; private set; }
        [field: SerializeField] public GameObject DeathBloodParticle { get; private set; }
    }
}
