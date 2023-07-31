using System;
using UnityEngine;

namespace Assets.Scripts.Weapons
{
    [Serializable]
    public class AttackActionHitBox : AttackData
    {
        public bool debug;
        [SerializeField] public Rect HitBox { get; private set; }
    }
}
