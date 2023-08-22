using System;
using UnityEngine;

namespace Assets.Scripts.Weapons
{
    [Serializable]
    public class AttackDraw : AttackData
    {
        [field: SerializeField] public AnimationCurve DrawCurve { get; private set; }
        [field: SerializeField] public float DrawTime { get; private set; }
    }
}
