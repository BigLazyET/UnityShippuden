using System;
using UnityEngine;

namespace Assets.Scripts.Weapons
{
    public class AttackChargeToProjectileSpawner : AttackData
    {
        [field: SerializeField, Range(0f, 360f)] public float AngleVariation { get; private set; }
    }
}
