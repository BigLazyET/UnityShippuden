using System;
using UnityEngine;

namespace Assets.Scripts.Weapons
{
    [Serializable]
    public class AttackDamage : AttackData
    {
        [field: SerializeField] public float Amount { get; private set; }
    }
}
