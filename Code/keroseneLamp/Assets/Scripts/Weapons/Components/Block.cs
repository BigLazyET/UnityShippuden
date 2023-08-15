using Assets.Scripts.CoreSystem;
using System;
using UnityEngine;

namespace Assets.Scripts.Weapons
{
    public class Block : WeaponComponent<BlockData, AttackBlock>
    {
        public event Action<GameObject> OnBlock;    // 参数是被Blocked的物体

        private DamageReceiver damageReceiver;
        private KnockBackReceiver knockBackReceiver;
        private poisonre
    }
}
