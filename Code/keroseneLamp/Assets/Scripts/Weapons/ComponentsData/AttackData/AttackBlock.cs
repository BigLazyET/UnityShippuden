using System;
using System.Linq;
using UnityEngine;

namespace Assets.Scripts.Weapons
{
    [Serializable]
    public class AttackBlock : AttackData
    {
        [field: SerializeField] public BlockDirectionInformation[] BlockDirectionInformation { get; private set; }

        public bool IsBlocked(float angle, out BlockDirectionInformation blockDirectionInformation)
        {
            blockDirectionInformation = BlockDirectionInformation.FirstOrDefault(x => x.IsAngleBetween(angle));

            return blockDirectionInformation != null;
        }
    }

    [Serializable]
    public class BlockDirectionInformation
    {
        [Range(-108f, 180f)] public float MinAngle;
        [Range(-180f, 180f)] public float MaxAngle;
        [Range(0f, 1f)] public float DamageAbsorption;  // Absorption: 吸收
        [Range(0f, 1f)] public float KnockBackAbsorption;
        [Range(0f, 1f)] public float PoiseDamageAbsorption;

        public bool IsAngleBetween(float angle)
        {
            if (MaxAngle > MinAngle)
                return angle >= MinAngle && angle <= MaxAngle;

            return (angle >= MinAngle && angle <= 180f) || (angle <= MaxAngle && angle >= -180f);
        }
    }
}
